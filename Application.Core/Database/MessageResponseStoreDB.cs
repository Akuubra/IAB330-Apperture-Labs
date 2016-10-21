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
    public class MessageResponseStoreDB : IMessageResponseStoreDatabase
    {

        public SQLiteConnection database;

        public MessageResponseStoreDB()
        {
            var sqlite = Mvx.Resolve<ISqlite>();
            database = sqlite.GetConnection();
            database.CreateTable<MessageResponseStore>();

        }

        public async Task<IEnumerable<MessageResponseStore>> GetResponses(string messageId)
        {
            return database.Table<MessageResponseStore>().Where(x => x.MessageID == messageId).ToList();
        }

        public async Task<int> InsertMessage(MessageResponseStore message)
        {
            var num = database.Insert(message);
            database.Commit();
            return num;
        }

        public async Task<int> DeleteMessage(object id)
        {
            return database.Delete<MessageResponseStore>(Convert.ToInt16(id));
        }


        
    }

}
