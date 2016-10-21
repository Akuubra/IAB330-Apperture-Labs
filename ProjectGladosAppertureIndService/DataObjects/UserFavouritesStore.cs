using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectGladosAppertureIndService.DataObjects
{
    public class UserFavouritesStore : EntityData
    {
        public string UserID { get; set; }
        public string FavouriteUserID { get; set; }

        public bool isFavourite { get; set; }
    }
}