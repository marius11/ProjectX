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
        public IEnumerable<Items> LoadAllItems()
        {
            using (ItemDBEntities entities = new ItemDBEntities())
            {
                return entities.Items.ToList();
            }
        }

        [HttpGet]
        public HttpResponseMessage LoadItemById(int id)
        {
            using (ItemDBEntities entities = new ItemDBEntities())
            {
                var entity = entities.Items.ToList().FirstOrDefault(i => i.Id == id);

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
        public HttpResponseMessage AddItem([FromBody] Items item)
        {
            try
            {
                using (ItemDBEntities entities = new ItemDBEntities())
                {
                    entities.Items.Add(item);
                    entities.SaveChanges();

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
        public HttpResponseMessage DeleteItemById(int id)
        {
            try
            {
                using (ItemDBEntities entities = new ItemDBEntities())
                {
                    var entity = entities.Items.FirstOrDefault(i => i.Id == id);

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
        public HttpResponseMessage UpdateItemById(int id, [FromBody] Items item)
        {
            try
            {
                using (ItemDBEntities entities = new ItemDBEntities())
                {
                    var entity = entities.Items.FirstOrDefault(i => i.Id == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item with id = " + id.ToString() + " not found");
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
