using Glados.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glados.Core.Interfaces
{
    public interface IUserFavouritesStoreDatabase
    {
        Task<IEnumerable<UserFavouritesStore>> GetFavourites(string userId);

        Task<UserFavouritesStore> GetFavourite(string userId, string favUserId);

        Task<int> DeleteFavourite(Object id);

        Task<int> InsertFavourite(string userID, string favouriteUserId, bool isFavourite);

        Task<int> UpdateFavourite(string userID, string favouriteUserId, bool isFavourite);

        Task<bool> favouriteExists(string userID, string favouriteID);
    }
}
