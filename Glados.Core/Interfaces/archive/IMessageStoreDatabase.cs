using Glados.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glados.Core.Interfaces
{
    public interface IMessageStoreDatabase
    {
        Task<IEnumerable<MessageRequestStore>> GetMessages();

        Task<int> DeleteMessage(string id);

        Task<int> InsertMessage(MessageRequestStore message);

        Task<int> UpdateMessage(MessageRequestStore message);

        Task<IEnumerable<MessageRequestStore>> GetUsersMessages(string id);
    }
}
