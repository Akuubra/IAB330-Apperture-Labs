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
    public class MessageResponseStoreController : TableController<MessageResponseStore>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MessageResponseStoreContext context = new MessageResponseStoreContext();
            DomainManager = new EntityDomainManager<MessageResponseStore>(context, Request);
        }

        // GET tables/MessageResponseStore
        public IQueryable<MessageResponseStore> GetAllMessageResponseStore()
        {
            return Query(); 
        }

        // GET tables/MessageResponseStore/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<MessageResponseStore> GetMessageResponseStore(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/MessageResponseStore/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<MessageResponseStore> PatchMessageResponseStore(string id, Delta<MessageResponseStore> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/MessageResponseStore
        public async Task<IHttpActionResult> PostMessageResponseStore(MessageResponseStore item)
        {
            MessageResponseStore current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/MessageResponseStore/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteMessageResponseStore(string id)
        {
             return DeleteAsync(id);
        }
    }
}
