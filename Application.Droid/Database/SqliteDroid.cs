using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System.IO;
using Application.Core.Interfaces;

namespace Application.Droid.Database
{
    public class SqliteDroid : ISqlite
    {
        public SQLiteConnection GetConnection()
        {
            //Call this in each platform before intializing your Mobile Client
            SQLitePCL.Batteries.Init();
            var sqliteFilename = "UserStoreSQLite.db3";

            string documnetsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documnetsPath, sqliteFilename);
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            //create the connection
            var conn = new SQLiteConnection(path); 

            // Return the database connection
            return conn;
        }
        
    }
}