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
    public class UserLoggedInDB : IUserLogin
    {

        public SQLiteConnection database;

        public UserLoggedInDB()
        {
            var sqlite = Mvx.Resolve<ISqlite>();
            database = sqlite.GetConnection();
            database.CreateTable<LoggedInUser>();
            
        }

        public async Task<IEnumerable<LoggedInUser>>  GetUsers()
        {
            return database.Table<LoggedInUser>().ToList();
        }

        public async Task<int> InsertUser(LoggedInUser user)
        {
            var num = database.Insert(user);
            database.Commit(); 
            return num; 
        }

        public async Task<int> DeleteUser(object id)
        {
            return database.Delete<LoggedInUser>(Convert.ToInt16(id));
        }

        public async Task<LoggedInUser> GetSingleUser(bool loggedIn)
        {
            var user = database.Table<LoggedInUser>().Where(x => x.LoggedIn == loggedIn);
            return user.FirstOrDefault();
        }

        public async Task<bool> IsUserLoggedIn(string userID)
        {
            var user = database.Table<LoggedInUser>().Where(x => x.UserId == userID);
            return user.FirstOrDefault().LoggedIn;
        }
        
    }
}
