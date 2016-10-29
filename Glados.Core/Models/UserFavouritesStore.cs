using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glados.Core.Models
{
    public class UserFavouritesStore
    {


        public string Id { get; set; }
        public string UserID { get; set; }
        public string FavouriteUserID { get; set; }

        public bool isFavourite { get; set; }




    }
}
