using Application.Core.Interfaces;
using Application.Core.Models;
using MvvmCross.Platform;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Database
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
           // var messages2 =  database.Table<MessageRequestStore>().Where(x => x.Sender == id).ToList();



            
          

          //  foreach (var message in messages)
          //  {
         //       messages.Add(message);
         //   }
         //   var messages3 = messages.OrderByDescending(x => x.UpdatedAt);
            return messages;

        }
    }

}
