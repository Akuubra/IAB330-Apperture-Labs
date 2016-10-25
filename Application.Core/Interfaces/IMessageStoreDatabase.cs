using Application.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces
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
