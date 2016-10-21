using Application.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces
{
    public interface IUserFavouritesStoreDatabase
    {
        Task<IEnumerable<UserFavouritesStore>> GetFavourites(string userId);

        Task<int> DeleteFavourite(Object id);

        Task<int> InsertFavourite(string userID, string favouriteUserId, bool isFavourite);

        Task<int> UpdateFavourite(string userID, string favouriteUserId, bool isFavourite);
    }
}
