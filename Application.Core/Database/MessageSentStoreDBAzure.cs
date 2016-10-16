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
    public class MessageSentStoreDBAzure : IMessageStoreDatabase
    {
        private MobileServiceClient azureDatabase;
        private IMobileServiceSyncTable<MessageSentStore> azureSyncTable;
   
        public MessageSentStoreDBAzure()
        {
            azureDatabase = Mvx.Resolve<IAzureDatabase>().GetMobileServiceClient();
            azureSyncTable = azureDatabase.GetSyncTable<MessageSentStore>();
        }

        public async Task<int> DeleteMessage(object id)
        {
            await SyncAsync(true);
            var messageSentStore = await azureSyncTable.Where(x => x.Id == (string)id).ToListAsync();
            if (messageSentStore.Any())
            {
                await azureSyncTable.DeleteAsync(messageSentStore.FirstOrDefault());
                await SyncAsync();
                return 1;
            }
            else
            {
                return 0;
            }


        }

        public async Task<int> UpdateMessage(MessageSentStore message)
        {
            await SyncAsync(true);
            var messageSentStore = await azureSyncTable.Where(x => x.Id == message.Id).ToListAsync();
            if (messageSentStore.Any())
            {
                await azureSyncTable.UpdateAsync(message);
                await SyncAsync();
                return 1;
            }
            else
            {
                return 0;
            }


        }

        public async Task<IEnumerable<MessageSentStore>> GetMessages()
        {
            await SyncAsync(true);
            var messages = await azureSyncTable.ToListAsync();
            return messages;
        }

        public async Task<int> InsertMessage(MessageSentStore message)
        {
            try
            {
                await SyncAsync(true);
                await azureSyncTable.InsertAsync(message);
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
                    await azureSyncTable.PullAsync("allMessages", azureSyncTable.CreateQuery());
                }
            }catch(Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}
