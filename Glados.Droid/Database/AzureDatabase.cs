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
using Glados.Core.Interfaces;
using Microsoft.WindowsAzure.MobileServices;
using System.IO;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Glados.Core.Models;
using System.Threading.Tasks;
using Glados.Droid.Views;

namespace Glados.Droid.Database
{
    public class AzureDatabase : IAzureDatabase
    {
        MobileServiceClient azureDatabse;

        public MobileServiceClient GetMobileServiceClient()
        {
            CurrentPlatform.Init();

            azureDatabse = new MobileServiceClient("http://projectgladosappertureind.azurewebsites.net/");
            InitializeLocal();
            return azureDatabse;
        }

        private void InitializeLocal()
        {

            //Call this in each platform before intializing your Mobile Client
            SQLitePCL.Batteries.Init();
            var sqliteFilename = "UserStoreSQLite.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<UserStore>();
            store.DefineTable<MessageRequestStore>();
            store.DefineTable<MessageResponseStore>();
            store.DefineTable<UserFavouritesStore>();
            azureDatabse.SyncContext.InitializeAsync(store);

            if (!File.Exists(path))
            {
               
            }
        }



    }
}