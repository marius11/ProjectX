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
            using (ItemDBEntities entities = new ItemDBEntities())
            {
                var items = await (from i in entities.Items
                             select new
                             {
                                 Id = i.Id,
                                 Text = i.Text
                             }).ToListAsync();

                return Request.CreateResponse(HttpStatusCode.OK, items);
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> LoadItemById(int id)
        {
            using (ItemDBEntities entities = new ItemDBEntities())
            {
                var entity = await entities.Items.SingleAsync(i => i.Id == id);

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
        public async Task<HttpResponseMessage> AddItem([FromBody] Items item)
        {
            try
            {
                using (ItemDBEntities entities = new ItemDBEntities())
                {
                    entities.Items.Add(item);
                    await entities.SaveChangesAsync();

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
                using (ItemDBEntities entities = new ItemDBEntities())
                {
                    var entity = await entities.Items.SingleAsync(i => i.Id == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Item with id {id} has not been found");
                    }
                    else
                    {
                        entities.Items.Remove(entity);
                        entities.SaveChanges();
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
        public async Task<HttpResponseMessage> UpdateItemById(int id, [FromBody] Items item)
        {
            try
            {
                using (ItemDBEntities entities = new ItemDBEntities())
                {
                    var entity = await entities.Items.SingleAsync(i => i.Id == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Item with id {id} has not been found");
                    }
                    else
                    {
                        entity.Text = item.Text;
                        entities.SaveChanges();

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
