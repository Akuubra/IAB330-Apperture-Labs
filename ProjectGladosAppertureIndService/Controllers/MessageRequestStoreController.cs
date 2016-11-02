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
    public class MessageRequestStoreController : TableController<MessageRequestStore>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MessageRequestStoreContext context = new MessageRequestStoreContext();
            DomainManager = new EntityDomainManager<MessageRequestStore>(context, Request);
        }

        // GET tables/MessageRequestStore
        public IQueryable<MessageRequestStore> GetAllMessageRequestStore()
        {
            return Query(); 
        }

        // GET tables/MessageRequestStore/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<MessageRequestStore> GetMessageRequestStore(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/MessageRequestStore/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<MessageRequestStore> PatchMessageRequestStore(string id, Delta<MessageRequestStore> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/MessageRequestStore
        public async Task<IHttpActionResult> PostMessageRequestStore(MessageRequestStore item)
        {
            MessageRequestStore current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/MessageRequestStore/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteMessageRequestStore(string id)
        {
             return DeleteAsync(id);
        }
    }
}
