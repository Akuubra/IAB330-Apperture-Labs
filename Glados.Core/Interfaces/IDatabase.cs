using Glados.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glados.Core.Interfaces
{
    public interface IDatabase
    {

        // Message Requests
        Task<IEnumerable<MessageRequestStore>> GetMessages();

        Task<int> DeleteMessage(string id);

        Task<int> InsertMessage(MessageRequestStore message);

        Task<int> UpdateMessage(MessageRequestStore message);

        Task<IEnumerable<MessageRequestStore>> GetUsersMessages(string id);



        /// Message Reponse Store
        Task<IEnumerable<MessageResponseStore>> GetResponses(string messageId);

        Task<int> DeleteMessage(Object id);

        Task<int> InsertMessage(MessageResponseStore response);
        Task<bool> IsResponded(string messageId, string receiverId);
        Task<MessageResponseStore> GetResponse(string messageId, string receiverId);


        // Favourites
        Task<IEnumerable<UserFavouritesStore>> GetFavourites(string userId);

        Task<UserFavouritesStore> GetFavourite(string userId, string favUserId);

        Task<int> DeleteFavourite(Object id);

        Task<int> InsertFavourite(string userID, string favouriteUserId, bool isFavourite);

        Task<int> UpdateFavourite(string userID, string favouriteUserId, bool isFavourite);

        Task<bool> favouriteExists(string userID, string favouriteID);


        // Users

        Task<IEnumerable<UserStore>> GetUsers();

        Task<int> DeleteUser(Object id);

        Task<int> InsertUser(UserStore user);

        Task<UserStore> GetSingleUser(string userID);
        Task<UserStore> GetSingleUserByName(string userName);

        Task<UserStore> GetUserLogin(string userName, string password);

    }
}
