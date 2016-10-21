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
   
    public class UserStoreController : TableController<UserStore>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            UserStoreContext context = new UserStoreContext();
            DomainManager = new EntityDomainManager<UserStore>(context, Request);
        }

        // GET tables/UserStore
        public IQueryable<UserStore> GetAllUserStore()
        {
            return Query(); 
        }

        // GET tables/UserStore/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<UserStore> GetUserStore(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/UserStore/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<UserStore> PatchUserStore(string id, Delta<UserStore> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/UserStore
        public async Task<IHttpActionResult> PostUserStore(UserStore item)
        {
            UserStore current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/UserStore/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteUserStore(string id)
        {
             return DeleteAsync(id);
        }
    }
}
