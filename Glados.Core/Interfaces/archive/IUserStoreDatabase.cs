using Glados.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glados.Core.Interfaces
{
    public interface IUserStoreDatabase
    {
        
        Task<IEnumerable<UserStore>> GetUsers();

        Task<int> DeleteUser(Object id);

        Task<int> InsertUser(UserStore user);

        Task<UserStore> GetSingleUser(string userID);
        Task<UserStore> GetSingleUserByName(string userName);

        Task<UserStore> GetUserLogin(string userName, string password);
    }
}
