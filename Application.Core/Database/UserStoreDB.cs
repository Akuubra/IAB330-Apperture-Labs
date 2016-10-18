using Application.Core.Interfaces;
using Application.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using MvvmCross.Platform;

namespace Application.Core.Database
{
    public class UserStoreDB : IUserStoreDatabase
    {

        public SQLiteConnection database;

        public UserStoreDB()
        {
            var sqlite = Mvx.Resolve<ISqlite>();
            database = sqlite.GetConnection();
            database.CreateTable<UserStore>();
            
        }

        public async Task<IEnumerable<UserStore>>  GetUsers()
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
    }
}
