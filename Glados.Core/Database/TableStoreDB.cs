using Glados.Core.Interfaces;
using Glados.Core.Models;
using MvvmCross.Platform;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glados.Core.Database
{
    public class TableStoreDB : IDatabase
    {

        public SQLiteConnection database;

        public TableStoreDB()
        {
            var sqlite = Mvx.Resolve<ISqlite>();
            database = sqlite.GetConnection();
            database.CreateTable<MessageRequestStore>();
            database.CreateTable<MessageResponseStore>();
            database.CreateTable<UserFavouritesStore>();
            database.CreateTable<UserStore>();

        }

        public async Task<IEnumerable<MessageRequestStore>> GetMessages()
        {
            return database.Table<MessageRequestStore>().ToList();
        }

        public async Task<int> InsertMessage(MessageRequestStore user)
        {
            var num = database.Insert(user);
            database.Commit();
            return num;
        }

        public async Task<int> DeleteMessage(string id)
        {
            var message = database.Table<MessageRequestStore>().Where(x => x.Id == id).ToList();
            return database.Delete<MessageRequestStore>(message.FirstOrDefault());
        }


        public async Task<int> UpdateMessage(MessageRequestStore message)
        {
            return database.Update(message);
        }


        public async Task<IEnumerable<MessageRequestStore>> GetUsersMessages(string id)
        {

            Debug.WriteLine(id);
            var messages = database.Table<MessageRequestStore>().Where(x => x.ReceivedBy == id || x.Sender == id).OrderByDescending(x => x.UpdatedAt).ToList();

            return messages;

        }


        public async Task<IEnumerable<MessageResponseStore>> GetResponses(string messageId)
        {
            return database.Table<MessageResponseStore>().Where(x => x.MessageID == messageId).ToList();
        }

        public async Task<int> InsertMessage(MessageResponseStore message)
        {
            var num = database.Insert(message);
            database.Commit();
            return num;
        }

        public async Task<bool> IsResponded(string messageId, string receiverId)
        {
            var messages = database.Table<MessageResponseStore>().Where(x => x.MessageID == messageId && x.Sender == receiverId).ToList();
            return messages.Any();
        }

        public async Task<MessageResponseStore> GetResponse(string messageId, string receiverId)
        {
            var messages = database.Table<MessageResponseStore>().Where(x => x.MessageID == messageId).ToList();
            return messages.FirstOrDefault();
        }

        public async Task<int> DeleteMessage(object id)
        {
            return database.Delete<MessageResponseStore>(Convert.ToInt16(id));
        }



        public async Task<IEnumerable<UserFavouritesStore>> GetFavourites(string userId)
        {
            var fav = database.Table<UserFavouritesStore>().Where(x => x.UserID == userId).ToList();
            return fav;
        }

        public async Task<int> InsertFavourite(string userID, string favouriteUserId, bool isFavourite)
        {
            UserFavouritesStore fav = new UserFavouritesStore();
            fav.UserID = userID;
            fav.FavouriteUserID = favouriteUserId;
            fav.isFavourite = isFavourite;

            var num = database.Insert(fav);
            database.Commit();
            return num;
        }

        public async Task<int> DeleteFavourite(object id)
        {
            return database.Delete<UserFavouritesStore>(Convert.ToInt16(id));
        }

        public async Task<int> UpdateFavourite(string userID, string favouriteUserId, bool isFavourite)
        {
            var fav = database.Table<UserFavouritesStore>().Where(x => x.UserID == userID & x.FavouriteUserID == favouriteUserId).ToList();
            var fav2 = fav.FirstOrDefault();
            fav2.isFavourite = isFavourite;
            database.Update(fav2);

            return 1;
        }

        public async Task<bool> favouriteExists(string userID, string favouriteID)
        {
            return database.Table<UserFavouritesStore>().Where(x => x.UserID == userID & x.FavouriteUserID == favouriteID).Any();
        }


        public async Task<UserFavouritesStore> GetFavourite(string userId, string favUserId)
        {
            var fav = database.Table<UserFavouritesStore>().Where(x => x.UserID == userId & x.FavouriteUserID == favUserId).ToList();

            return fav.FirstOrDefault();

        }


        public async Task<IEnumerable<UserStore>> GetUsers()
        {
            return database.Table<UserStore>().ToList();
        }

        public async Task<int> InsertUser(UserStore user)
        {
            var num = database.Insert(user);
            database.Commit();
            return num;
        }

        public async Task<int> DeleteUser(object id)
        {
            return database.Delete<UserStore>(Convert.ToInt16(id));
        }

        public async Task<UserStore> GetSingleUser(string userID)
        {
            var user = database.Table<UserStore>().Where(x => x.Id == userID);
            return user.FirstOrDefault();
        }

        public async Task<UserStore> GetSingleUserByName(string userName)
        {

            var user = database.Table<UserStore>().ToList();
            user.Where(x => x.Username == userName);
            return user.FirstOrDefault();
        }
        public async Task<UserStore> GetUserLogin(string userName, string password)
        {
            var user = database.Table<UserStore>().ToList();
            user.Where(x => x.Username == userName & x.Password == password);
            return user.FirstOrDefault();
        }
    }

}
