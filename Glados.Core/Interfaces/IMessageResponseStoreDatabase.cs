using Glados.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glados.Core.Interfaces
{
    public interface IMessageResponseStoreDatabase
    {
        Task<IEnumerable<MessageResponseStore>> GetResponses(string messageId);

        Task<int> DeleteMessage(Object id);

        Task<int> InsertMessage(MessageResponseStore response);
        Task<bool> IsResponded(string messageId, string receiverId);

    }
}
