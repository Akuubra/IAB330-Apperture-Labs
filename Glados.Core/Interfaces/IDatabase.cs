using Glados.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glados.Core.Interfaces
{/// <summary>
/// All Database fucntionality to all tables in azure and locatlly
/// 
/// Author: Jared bagnall 
/// </summary>
    public interface IDatabase
    {

        // Message Requests
        /// <summary>
        /// Gets the full list of messages available in the database
        /// </summary>
        /// <returns> Collection of MessageRequestStore Messages</returns>
        Task<IEnumerable<MessageRequestStore>> GetMessages();

        /// <summary>
        /// Deletes the provided message
        /// </summary>
        /// <param name="id">Id number of the message to be deleted</param>
        /// <returns>int value if successfull</returns>
        Task<int> DeleteMessage(string id);


        /// <summary>
        /// Insterts the requested message into the database
        /// </summary>
        /// <param name="message">message to be inserted into the database</param>
        /// <returns>number of inserts</returns>
        Task<int> InsertMessage(MessageRequestStore message);

        /// <summary>
        /// Updates the requested message with the new data
        /// </summary>
        /// <param name="message"> message to be updated</param>
        /// <returns>int value if successful</returns>
        Task<int> UpdateMessage(MessageRequestStore message);

        /// <summary>
        /// gets a list of the messages relating to the provided user id
        /// </summary>
        /// <param name="id"> user id of the user </param>
        /// <returns> Collection of MessageRequestStore Messages</returns>
        Task<IEnumerable<MessageRequestStore>> GetUsersMessages(string id);


        /// Message Reponse Store
        /// <summary>
        /// Gets a list of the responses received for the messageId requested
        /// </summary>
        /// <param name="messageId">Id number of the message</param>
        /// <returns>Collection of MessageResponseStore responses</returns>
        Task<IEnumerable<MessageResponseStore>> GetResponses(string messageId);

        /// <summary>
        /// Deletes the requested response
        /// </summary>
        /// <param name="id">Id of the response to delete</param>
        /// <returns>int value if successfull</returns>
        Task<int> DeleteMessage(Object id);

        /// <summary>
        /// Inserts the requested response
        /// </summary>
        /// <param name="response">Resposne to be inserted</param>
        /// <returns>number of inserts</returns>
        Task<int> InsertMessage(MessageResponseStore response);

        /// <summary>
        /// Check to see if message has a response for the requested user
        /// </summary>
        /// <param name="messageId"> message id of the message</param>
        /// <param name="receiverId">userId of the receiver</param>
        /// <returns>true if response is found</returns>
        Task<bool> IsResponded(string messageId, string receiverId);

        /// <summary>
        /// Gets a response for the requested message by the requested user
        /// </summary>
        /// <param name="messageId">Message id of the message</param>
        /// <param name="receiverId">user id of the receiver</param>
        /// <returns>MessageResponseStore response </returns>
        Task<MessageResponseStore> GetResponse(string messageId, string receiverId);


        // Favourites

        /// <summary>
        /// gets the faourites relevant to the user requested
        /// </summary>
        /// <param name="userId">user id of the user</param>
        /// <returns>Collection of UserFavouritesStore favourites</returns>
        Task<IEnumerable<UserFavouritesStore>> GetFavourites(string userId);

        /// <summary>
        /// Gets a specific favourite detail for a user
        /// </summary>
        /// <param name="userId">user id of user</param>
        /// <param name="favUserId"></param>
        /// <returns></returns>
        Task<UserFavouritesStore> GetFavourite(string userId, string favUserId);

        /// <summary>
        /// Deletes the requested favourite
        /// </summary>
        /// <param name="id">id of the favrourite to be deleted</param>
        /// <returns>int value if successfull</returns>
        Task<int> DeleteFavourite(Object id);

        /// <summary>
        /// Inserts a favourite of the requesed user for the requested favouriteUser
        /// </summary>
        /// <param name="userID">user id of the user creating the favourite</param>
        /// <param name="favouriteUserId">user id of the user being favourited</param>
        /// <param name="isFavourite">whether the user is adding or removing the favourite</param>
        /// <returns>int value of number of inserts</returns>
        Task<int> InsertFavourite(string userID, string favouriteUserId, bool isFavourite);

        /// <summary>
        /// updates a favourite of the requesed user for the requested favouriteUser
        /// </summary>
        /// <param name="userID">user id of the user updating the favourite</param>
        /// <param name="favouriteUserId">user id of the user being favourited</param>
        /// <param name="isFavourite">whether the user is adding or removing the favourite</param>
        /// <returns>int value of number of updates</returns>
        Task<int> UpdateFavourite(string userID, string favouriteUserId, bool isFavourite);

        /// <summary>
        /// Checks to see if a favourite exists
        /// </summary>
        /// <param name="userID">user id of user </param>
        /// <param name="favouriteID">favourite id </param>
        /// <returns>true if favourite found</returns>
        Task<bool> favouriteExists(string userID, string favouriteID);


        // Users
        /// <summary>
        /// gets a list of the users in the database
        /// </summary>
        /// <returns>Collection of users</returns>
        Task<IEnumerable<UserStore>> GetUsers();
        
        /// <summary>
        /// deletes the requested user
        /// </summary>
        /// <param name="id">user object to be deleted</param>
        /// <returns>number of users deleted</returns>
        Task<int> DeleteUser(Object id);
        
        /// <summary>
        /// Inserts the requested user into the database
        /// </summary>
        /// <param name="user">user to be inserted</param>
        /// <returns>number of users inserted</returns>
        Task<int> InsertUser(UserStore user);

        /// <summary>
        /// Gets the requested user by userid
        /// </summary>
        /// <param name="userID">user id of requested user</param>
        /// <returns>UserStore user</returns>
        Task<UserStore> GetSingleUser(string userID);

        /// <summary>
        /// Gets the requested user by username
        /// </summary>
        /// <param name="userName">Username of user requested</param>
        /// <returns>UserStore user</returns>
        Task<UserStore> GetSingleUserByName(string userName);

        /// <summary>
        /// Gets the requested user when the password also matches
        /// </summary>
        /// <param name="userName">user name of the user reequested</param>
        /// <param name="password">passsword for the user</param>
        /// <returns>UserStore user</returns>
        Task<UserStore> GetUserLogin(string userName, string password);

    }
}
