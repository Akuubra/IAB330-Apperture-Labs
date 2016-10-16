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
        Task<IEnumerable<MessageSentStore>> GetMessages();

        Task<int> DeleteMessage(Object id);

        Task<int> InsertMessage(MessageSentStore message);

        Task<int> UpdateMessage(MessageSentStore message);
    }
}
