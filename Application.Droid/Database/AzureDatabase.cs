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
using Application.Core.Interfaces;
using Microsoft.WindowsAzure.MobileServices;
using System.IO;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Application.Core.Models;

namespace Application.Droid.Database
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
            var sqliteFilename = "UserStoreSQLite.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<UserStore>();
            store.DefineTable<MessageSentStore>();
            azureDatabse.SyncContext.InitializeAsync(store);
        }
    }
}