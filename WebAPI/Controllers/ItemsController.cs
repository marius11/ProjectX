using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ItemDataAccess;

namespace WebAPI.Controllers
{
    public class ItemsController : ApiController
    {
        [HttpGet]
        public IEnumerable<Item> LoadAllItems()
        {
            using (TestDBEntities entities = new TestDBEntities())
            {
                return entities.Items.ToList();
            }
        }

        [HttpGet]
        public HttpResponseMessage LoadItemById(int id)
        {
            using (TestDBEntities entities = new TestDBEntities())
            {
                var entity = entities.Items.ToList().FirstOrDefault(i => i.id == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item with id = " + id.ToString() + " not found");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
        }

        [HttpPost]
        public HttpResponseMessage AddItem([FromBody] Item item)
        {
            try
            {
                using (TestDBEntities entities = new TestDBEntities())
                {
                    entities.Items.Add(item);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, item);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + item.id.ToString());
                    return message;
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteItemById(int id)
        {
            try
            {
                using (TestDBEntities entities = new TestDBEntities())
                {
                    var entity = entities.Items.FirstOrDefault(i => i.id == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item with id = " + id.ToString() + " not found");
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
        public HttpResponseMessage UpdateItemById(int id, [FromBody] Item item)
        {
            try
            {
                using (TestDBEntities entities = new TestDBEntities())
                {
                    var entity = entities.Items.FirstOrDefault(i => i.id == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item with id = " + id.ToString() + " not found");
                    }
                    else
                    {
                        entity.text = item.text;

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
