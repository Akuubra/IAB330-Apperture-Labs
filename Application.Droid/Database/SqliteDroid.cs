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
            var sqliteFilename = "UserStoreSQLite.db3";

            string documnetsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documnetsPath, sqliteFilename);
            
            //create the connection
            var conn = new SQLiteConnection(path); 

            // Return the database connection
            return conn;
        }
        
    }
}