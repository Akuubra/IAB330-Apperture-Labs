using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using ProjectGladosAppertureIndService.DataObjects;
using ProjectGladosAppertureIndService.Models;

namespace ProjectGladosAppertureIndService.Controllers
{
    public class UserFavouritesStoreController : TableController<UserFavouritesStore>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            UserFavouritesStoreContext context = new UserFavouritesStoreContext();
            DomainManager = new EntityDomainManager<UserFavouritesStore>(context, Request);
        }

        // GET tables/UserFavouritesStore
        public IQueryable<UserFavouritesStore> GetAllUserFavouritesStore()
        {
            return Query(); 
        }

        // GET tables/UserFavouritesStore/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<UserFavouritesStore> GetUserFavouritesStore(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/UserFavouritesStore/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<UserFavouritesStore> PatchUserFavouritesStore(string id, Delta<UserFavouritesStore> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/UserFavouritesStore
        public async Task<IHttpActionResult> PostUserFavouritesStore(UserFavouritesStore item)
        {
            UserFavouritesStore current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/UserFavouritesStore/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteUserFavouritesStore(string id)
        {
             return DeleteAsync(id);
        }
    }
}
