﻿using Glados.Core.Interfaces;
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
    public class MessageRequestStoreDB : IMessageStoreDatabase
    {

        public SQLiteConnection database;

        public MessageRequestStoreDB()
        {
            var sqlite = Mvx.Resolve<ISqlite>();
            database = sqlite.GetConnection();
            database.CreateTable<MessageRequestStore>();

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
            var messages =  database.Table<MessageRequestStore>().Where(x => x.ReceivedBy == id || x.Sender == id).OrderByDescending(x => x.UpdatedAt).ToList();

            return messages;

        }
    }

}
