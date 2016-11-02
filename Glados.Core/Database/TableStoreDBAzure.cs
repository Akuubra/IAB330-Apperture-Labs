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
    public class TableStoreDBAzure : IDatabase
    {
        private MobileServiceClient azureDatabase;
        private IMobileServiceSyncTable<MessageRequestStore> messageRequestSyncTable;
        private IMobileServiceSyncTable<MessageResponseStore>  messageResponseSyncTable;
        private IMobileServiceSyncTable<UserFavouritesStore> favSyncTable;
        private IMobileServiceSyncTable<UserStore> userSyncTable;


        public TableStoreDBAzure()
        {
            azureDatabase = Mvx.Resolve<IAzureDatabase>().GetMobileServiceClient();
            messageRequestSyncTable = azureDatabase.GetSyncTable<MessageRequestStore>();
            messageResponseSyncTable = azureDatabase.GetSyncTable<MessageResponseStore>();
            favSyncTable = azureDatabase.GetSyncTable<UserFavouritesStore>();
            userSyncTable = azureDatabase.GetSyncTable<UserStore>();
        }

        public async Task<int> DeleteMessage(string id)
        {
            await SyncAsync(true);
            var messageSentStore = await messageRequestSyncTable.Where(x => x.Id == id).ToListAsync();
            if (messageSentStore.Any())
            {
                await messageRequestSyncTable.DeleteAsync(messageSentStore.FirstOrDefault());
                await SyncAsync();
                return 1;
            }
            else
            {
                return 0;
            }


        }

        public async Task<int> UpdateMessage(MessageRequestStore message)
        {
            await SyncAsync(true);
            var messageRequestStore = await messageRequestSyncTable.Where(x => x.Id == message.Id).ToListAsync();
            if (messageRequestStore.Any())
            {
                await SyncAsync(true);
                await messageRequestSyncTable.UpdateAsync(message);
                await SyncAsync();
                return 1;
            }
            else
            {
                return 0;
            }


        }

        public async Task<IEnumerable<MessageRequestStore>> GetMessages()
        {
            await SyncAsync(true);
            var messages = await messageRequestSyncTable.ToListAsync();
            return messages;
        }

        public async Task<int> InsertMessage(MessageRequestStore message)
        {
            try
            {
                await SyncAsync(true);
                await messageRequestSyncTable.InsertAsync(message);
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
                    await messageRequestSyncTable.PullAsync("allMessagesRequests", messageRequestSyncTable.CreateQuery());
                }
            }catch(Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public async Task<IEnumerable<MessageRequestStore>> GetUsersMessages(string id)
        {

            await SyncAsync(true);
            var messages = await messageRequestSyncTable.Where(x => x.ReceivedBy == id || x.Sender == id).OrderByDescending(x=> x.UpdatedAt).ToListAsync();

            return messages;

        }


















        public async Task<int> DeleteMessage(object id)
        {
            await ResponseSyncAsync(true);
            var messageResponseStore = await messageResponseSyncTable.Where(x => x.Id == (string)id).ToListAsync();
            if (messageResponseStore.Any())
            {
                await messageResponseSyncTable.DeleteAsync(messageResponseStore.FirstOrDefault());
                await ResponseSyncAsync();
                return 1;
            }
            else
            {
                return 0;
            }


        }



        public async Task<IEnumerable<MessageResponseStore>> GetResponses(string messageId)
        {
            await ResponseSyncAsync(true);
            var messages = await messageResponseSyncTable.Where(x => x.MessageID == messageId).ToListAsync();
            return messages;
        }

        public async Task<int> InsertMessage(MessageResponseStore message)
        {
            try
            {
                await ResponseSyncAsync(true);
                await messageResponseSyncTable.InsertAsync(message);
                await ResponseSyncAsync();
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
            await ResponseSyncAsync(true);
            var messages = await messageResponseSyncTable.Where(x => x.MessageID == messageId && x.Sender == receiverId).ToListAsync();
            return messages.Any();
        }


        public async Task<MessageResponseStore> GetResponse(string messageId, string receiverId)
        {
            await ResponseSyncAsync(true);
            var messages = await messageResponseSyncTable.Where(x => x.MessageID == messageId && x.Sender == receiverId).ToListAsync();
            return messages.FirstOrDefault();
        }

        private async Task ResponseSyncAsync(bool pullData = false)
        {
            try
            {
                await azureDatabase.SyncContext.PushAsync();
                if (pullData)
                {
                    await messageResponseSyncTable.PullAsync("allMessagesResponses", messageResponseSyncTable.CreateQuery());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

















        public async Task<int> DeleteFavourite(object id)
        {
            await FavSyncAsync(true);
            var userStore = await favSyncTable.Where(x => x.Id == (string)id).ToListAsync();
            if (userStore.Any())
            {
                await FavSyncAsync(true);
                await favSyncTable.DeleteAsync(userStore.FirstOrDefault());
                await FavSyncAsync();
                return 1;
            }
            else
            {
                return 0;
            }


        }


        public async Task<IEnumerable<UserFavouritesStore>> GetFavourites(string userId)
        {
            await FavSyncAsync(true);
            var fav = await favSyncTable.Where(x => x.UserID == userId).ToListAsync();

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
                await FavSyncAsync(true);
                await favSyncTable.InsertAsync(fav);
                await FavSyncAsync();
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
                await FavSyncAsync(true);
                var fav = await favSyncTable.Where(x => x.UserID == userID & x.FavouriteUserID == favouriteUserId).ToListAsync();

                var fav2 = fav.FirstOrDefault();
                fav2.isFavourite = isFavourite;

                await favSyncTable.UpdateAsync(fav2);
                await FavSyncAsync();
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
            var fav = await favSyncTable.Where(x => x.UserID == userId & x.FavouriteUserID == favUserId).ToListAsync();

            return fav.FirstOrDefault();

        }

        private async Task FavSyncAsync(bool pullData = false)
        {
            try
            {
                await azureDatabase.SyncContext.PushAsync();
                if (pullData)
                {
                    await favSyncTable.PullAsync("allFavourites", favSyncTable.CreateQuery());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

















        public async Task<int> DeleteUser(object id)
        {
            await UserSyncAsync(true);

            var userStore = await userSyncTable.Where(x => x.Id == (string)id).ToListAsync();

            if (userStore.Any())
            {
                await UserSyncAsync(true);

                await userSyncTable.DeleteAsync(userStore.FirstOrDefault());
                await UserSyncAsync();
                return 1;
            }
            else
            {
                return 0;
            }


        }


        public async Task<UserStore> GetSingleUser(string userID)
        {
            await UserSyncAsync(true);
            var user = await userSyncTable.Where(x => x.Id == userID).ToListAsync();

            return user.FirstOrDefault();
        }

        public async Task<UserStore> GetSingleUserByName(string userName)
        {
            await UserSyncAsync(true);
            var user = await userSyncTable.Where(x => x.Username == userName).ToListAsync();

            return user.FirstOrDefault();
        }

        public async Task<UserStore> GetUserLogin(string userName, string password)
        {
            await UserSyncAsync(true);
            var user = await userSyncTable.Where(x => x.Username == userName & x.Password == password).ToListAsync();

            return user.FirstOrDefault();
        }

        public async Task<IEnumerable<UserStore>> GetUsers()
        {
            await UserSyncAsync(true);
            var users = await userSyncTable.ToListAsync();
            return users;
        }

        public async Task<int> InsertUser(UserStore user)
        {
            try
            {
                await UserSyncAsync(true);
                await userSyncTable.InsertAsync(user);
                await UserSyncAsync();
                return 1;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return 0;

        }


        private async Task UserSyncAsync(bool pullData = false)
        {
            try
            {
                await azureDatabase.SyncContext.PushAsync();
                if (pullData)
                {
                    await userSyncTable.PullAsync("allUsers", userSyncTable.CreateQuery());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

    }
}
