﻿using Presentation.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class ItemsController : Controller
    {
        private static readonly string baseURI = "https://localhost:44355/api/items";

        public ActionResult Index()
        {
            return View();
        }

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