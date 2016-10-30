using Glados.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glados.Core.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MvvmCross.Platform;
using System.Diagnostics;

namespace Glados.Core.Database
{
    public class MessageResponseStoreDBAzure : IMessageResponseStoreDatabase
    {
        private MobileServiceClient azureDatabase;
        private IMobileServiceSyncTable<MessageResponseStore> azureSyncTable;
    
        public MessageResponseStoreDBAzure()
        {
            azureDatabase = Mvx.Resolve<IAzureDatabase>().GetMobileServiceClient();
            azureSyncTable = azureDatabase.GetSyncTable<MessageResponseStore>();
        }

        public async Task<int> DeleteMessage(object id)
        {
            await SyncAsync(true);
            var messageResponseStore = await azureSyncTable.Where(x => x.Id == (string)id).ToListAsync();
            if (messageResponseStore.Any())
            {
                await azureSyncTable.DeleteAsync(messageResponseStore.FirstOrDefault());
                await SyncAsync();
                return 1;
            }
            else
            {
                return 0;
            }


        }



        public async Task<IEnumerable<MessageResponseStore>> GetResponses(string messageId)
        {
            await SyncAsync(true);
            var messages = await azureSyncTable.Where(x => x.MessageID == messageId).ToListAsync();
            return messages;
        }

        public async Task<int> InsertMessage(MessageResponseStore message)
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


        public async Task<bool> IsResponded(string messageId, string receiverId)
        {
            await SyncAsync(true);
            var messages = await azureSyncTable.Where(x => x.MessageID == messageId || x.Sender == receiverId).ToListAsync();
            return messages.Any();
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
