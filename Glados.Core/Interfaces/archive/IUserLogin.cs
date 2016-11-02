using Glados.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glados.Core.Interfaces
{
    public interface IUserLogin
    {
        Task<int> InsertUser(LoggedInUser user);

        Task<int> DeleteUser(object id);

        Task<LoggedInUser> GetSingleUser(bool loggedIn);

        Task<bool> IsUserLoggedIn(string userID);

    }
}
