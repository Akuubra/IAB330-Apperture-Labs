using Application.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces
{
    public interface IMessageResponseStoreDatabase
    {
        Task<IEnumerable<MessageResponseStore>> GetResponses(string messageId);

        Task<int> DeleteMessage(Object id);

        Task<int> InsertMessage(MessageResponseStore response);
        
    }
}
