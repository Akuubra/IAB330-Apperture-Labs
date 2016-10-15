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

            //UserStore user = new UserStore();
            //user.Email = "Jared@jared.com";
            //user.First_Name = "Jared";
            //user.Last_Name = "Bagnall";
            //user.Location = "Level 11";
            //user.Username = "deraj";

            //database.Insert(user);
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
       
        
    }
}
