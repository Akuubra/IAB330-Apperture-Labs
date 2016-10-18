using Application.Core.Interfaces;
using Application.Core.Models;
using MvvmCross.Platform;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Database
{
    public class MessageSentStoreDB : IMessageStoreDatabase
    {

        public SQLiteConnection database;

        public MessageSentStoreDB()
        {
            var sqlite = Mvx.Resolve<ISqlite>();
            database = sqlite.GetConnection();
            database.CreateTable<MessageSentStore>();

        }

        public async Task<IEnumerable<MessageSentStore>> GetMessages()
        {
            return database.Table<MessageSentStore>().ToList();
        }

        public async Task<int> InsertMessage(MessageSentStore user)
        {
            var num = database.Insert(user);
            database.Commit();
            return num;
        }

        public async Task<int> DeleteMessage(object id)
        {
            return database.Delete<MessageSentStore>(Convert.ToInt16(id));
        }


        public async Task<int> UpdateMessage(MessageSentStore message)
        {
            return database.Update(message);
        }


        public async Task<IEnumerable<MessageSentStore>> GetUsersMessages(string id)
        {

            Debug.WriteLine(id);
            var messages =  database.Table<MessageSentStore>().Where(x => x.ReceivedBy == id).ToList();
            var messages2 =  database.Table<MessageSentStore>().Where(x => x.Sender == id).ToList();

            foreach (var message in messages2)
            {
                messages.Add(message);
            }
            return messages;

        }
    }

}
