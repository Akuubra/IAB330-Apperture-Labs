﻿using Application.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MvvmCross.Platform;
using System.Diagnostics;

namespace Application.Core.Database
{
    public class UserStoreDBAzure : IUserStoreDatabase
    {
        private MobileServiceClient azureDatabase;
        private IMobileServiceSyncTable<UserStore> azureSyncTable;
   
        public UserStoreDBAzure()
        {
            azureDatabase = Mvx.Resolve<IAzureDatabase>().GetMobileServiceClient();
            azureSyncTable = azureDatabase.GetSyncTable<UserStore>();
        }

        public async Task<int> DeleteUser(object id)
        {
            await SyncAsync(true);
            var userStore = await azureSyncTable.Where(x => x.Id == (string)id).ToListAsync();
            if (userStore.Any())
            {
                await azureSyncTable.DeleteAsync(userStore.FirstOrDefault());
                await SyncAsync();
                return 1;
            }
            else
            {
                return 0;
            }


        }


        public async Task<UserStore> GetSingleUser(string userID)
        {
            await SyncAsync(true);
            var user = await azureSyncTable.Where(x => x.Id == userID).ToListAsync();

            return user.FirstOrDefault();
        }

        public async Task<UserStore> GetSingleUserByName(string userName)
        {
            await SyncAsync(true);
            var user = await azureSyncTable.Where(x => x.Username == userName).ToListAsync();

            return user.FirstOrDefault();
        }

        public async Task<IEnumerable<UserStore>> GetUsers()
        {
            await SyncAsync(true);
            var users = await azureSyncTable.ToListAsync();
            return users;
        }

        public async Task<int> InsertUser(UserStore user)
        {
            try
            {
                await SyncAsync(true);
                await azureSyncTable.InsertAsync(user);
                await SyncAsync();
                return 1;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return 0;

        }


        private async Task SyncAsync(bool pullData = false)
        {
            try
            {
                await azureDatabase.SyncContext.PushAsync();
                if (pullData)
                {
                    await azureSyncTable.PullAsync("allUsers", azureSyncTable.CreateQuery());
                }
            }catch(Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}