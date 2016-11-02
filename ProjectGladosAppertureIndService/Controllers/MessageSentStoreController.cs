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
  
    public class MessageSentStoreController : TableController<MessageSentStore>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MessagesSentStoreContext context = new MessagesSentStoreContext();
            DomainManager = new EntityDomainManager<MessageSentStore>(context, Request);
        }

        // GET tables/MessageSentStore
        public IQueryable<MessageSentStore> GetAllMessageSentStore()
        {
            return Query(); 
        }

        // GET tables/MessageSentStore/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<MessageSentStore> GetMessageSentStore(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/MessageSentStore/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<MessageSentStore> PatchMessageSentStore(string id, Delta<MessageSentStore> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/MessageSentStore
        public async Task<IHttpActionResult> PostMessageSentStore(MessageSentStore item)
        {
            MessageSentStore current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/MessageSentStore/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteMessageSentStore(string id)
        {
             return DeleteAsync(id);
        }
    }
}
