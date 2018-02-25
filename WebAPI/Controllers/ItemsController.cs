using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ItemDataAccess;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WebAPI.Controllers
{
    public class ItemsController : ApiController
    {
        [HttpGet]
        public async Task<HttpResponseMessage> LoadAllItems()
        {
            using (ProjectXContext db = new ProjectXContext())
            {
                var items = await
                    (from i in db.Items
                     select new
                     {
                         i.Id,
                         i.Text
                     }).ToListAsync();

                return Request.CreateResponse(HttpStatusCode.OK, items);
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> LoadItemById(int id)
        {
            using (ProjectXContext db = new ProjectXContext())
            {
                var entity = await db.Items.SingleAsync(i => i.Id == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Item with id {id} has not been found");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddItem([FromBody] Item item)
        {
            try
            {
                using (ProjectXContext db = new ProjectXContext())
                {
                    db.Items.Add(item);
                    await db.SaveChangesAsync();

                    var message = Request.CreateResponse(HttpStatusCode.Created, item);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + item.Id.ToString());
                    return message;
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteItemById(int id)
        {
            try
            {
                using (ProjectXContext db = new ProjectXContext())
                {
                    var entity = await db.Items.SingleAsync(i => i.Id == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Item with id {id} has not been found");
                    }
                    else
                    {
                        db.Items.Remove(entity);
                        await db.SaveChangesAsync();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateItemById(int id, [FromBody] Item item)
        {
            try
            {
                using (ProjectXContext db = new ProjectXContext())
                {
                    var entity = await db.Items.SingleAsync(i => i.Id == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Item with id {id} has not been found");
                    }
                    else
                    {
                        entity.Text = item.Text;
                        await db.SaveChangesAsync();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
