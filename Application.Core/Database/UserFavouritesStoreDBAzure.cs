using Application.Core.Interfaces;
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
    public class UserFavouritesStoreDBAzure : IUserFavouritesStoreDatabase
    {
        private MobileServiceClient azureDatabase;
        private IMobileServiceSyncTable<UserFavouritesStore> azureSyncTable;
   
        public UserFavouritesStoreDBAzure()
        {
            azureDatabase = Mvx.Resolve<IAzureDatabase>().GetMobileServiceClient();
            azureSyncTable = azureDatabase.GetSyncTable<UserFavouritesStore>();
        }

        public async Task<int> DeleteFavourite(object id)
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


        public async Task<IEnumerable<UserFavouritesStore>> GetFavourites(string userId)
        {
            await SyncAsync(true);
            var fav = await azureSyncTable.Where(x => x.UserID == userId).ToListAsync();

            return fav;
        }

      

        public async Task<int> InsertFavourite(string userID, string favouriteUserId, bool isFavourite)
        {
            UserFavouritesStore fav = new UserFavouritesStore();
            fav.UserID = userID;
            fav.FavouriteUserID = favouriteUserId;
            fav.isFavourite = isFavourite; 
            try
            {
                await SyncAsync(true);
                await azureSyncTable.InsertAsync(fav);
                await SyncAsync();
                return 1;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return 0;

        }

        public async Task<int> UpdateFavourite(string userID, string favouriteUserId, bool isFavourite)
        {
           try
            {
                await SyncAsync(true);
                var fav = await azureSyncTable.Where(x=> x.UserID == userID & x.FavouriteUserID == favouriteUserId).ToListAsync();
                
                var fav2 = fav.FirstOrDefault(); 
                fav2.isFavourite = isFavourite;

                await azureSyncTable.UpdateAsync(fav2);
                await SyncAsync();
                return 1;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return 0;

        }

        public async Task<bool> favouriteExists(string userID, string favouriteID)
        {
            var fav = await GetFavourite(userID, favouriteID);
            var FavExists = fav != null;
            return FavExists;
        }


        public async Task<UserFavouritesStore> GetFavourite(string userId, string favUserId)
        {
            var fav = await azureSyncTable.Where(x => x.UserID == userId & x.FavouriteUserID == favUserId).ToListAsync();

            return fav.FirstOrDefault();

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
