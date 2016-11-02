using Glados.Core.Interfaces;
using Glados.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MvvmCross.Platform;

namespace Glados.Core.Database
{
    public class UserFavouritesStoreDB : IUserFavouritesStoreDatabase
    {

        public SQLiteConnection database;

        public UserFavouritesStoreDB()
        {
            var sqlite = Mvx.Resolve<ISqlite>();
            database = sqlite.GetConnection();
            database.CreateTable<UserFavouritesStore>();

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
    }
}
