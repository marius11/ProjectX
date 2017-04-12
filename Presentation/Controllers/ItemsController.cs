using Microsoft.Ajax.Utilities;
using Presentation.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class ItemsController : Controller
    {
        private static readonly string baseURI = "http://localhost:64307/api/items";

        public ActionResult Index()
        {
            return View();
        }

        // GET: Item
        [HttpGet]
        [Route("items")]
        public async Task<ActionResult> GetItems()
        {
            using (HttpClient client = new HttpClient())
            {
                IEnumerable<Item> items = null;
                var response = await client.GetAsync(baseURI);

                if (response.IsSuccessStatusCode)
                {
                    items = await response.Content.ReadAsAsync<IEnumerable<Item>>();
                }

                items.ForEach(i => Trace.WriteLine(i.id + ", " + i.text));

                return View(items);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteItems(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync(baseURI + "/" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpPut]
        public async Task<ActionResult> EditItem(int id, Item item)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PutAsJsonAsync(baseURI + "/" + id.ToString(), item);

                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateItem(Item item)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(baseURI, item);

                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}