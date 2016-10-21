using Application.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces
{
    public interface IUserLogin
    {
        Task<int> InsertUser(LoggedInUser user);

        Task<int> DeleteUser(object id);

        Task<LoggedInUser> GetSingleUser(bool loggedIn);

        Task<bool> IsUserLoggedIn(string userID);

    }
}
