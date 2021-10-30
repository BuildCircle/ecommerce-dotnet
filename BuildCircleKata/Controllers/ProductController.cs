using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace BuildCircleKata.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : Controller
    {
        public List<Product> getAllItems()
        {
            using (StreamReader r = new StreamReader("./items.json"))
            {
                string json = r.ReadToEnd();
                var products = JsonConvert.DeserializeObject<Products>(json);
                List<Product> productList = products.Items;
                return productList;
            }
        }

        public Product loadById(int? id)
        {
            var item = getAllItems().FirstOrDefault(t => t.id == id);
            return item;
        }

        public Product loadByName(string name)
        {
            var item = getAllItems().FirstOrDefault(t => t.name == name);
            return item;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(getAllItems());
        }

        [HttpGet("product/{id}")]
        public IActionResult GetId(int? id)
        {
            var product = loadById(id);
            return product == null
                ? NotFound()
                : Ok(product);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name)
        {
            var product = loadByName(name);
            return product == null
                ? NotFound()
                : Ok(product);
        }

        [HttpGet("tag/{tag}")]
        public IActionResult GetByTags(string tag)
        {
            var productsList = getAllItems();

            var products = productsList.Where(product => product.tags.Contains(tag)).ToList();

            return products.Any() 
                ? Ok(products) 
                : NotFound();
        }

    }
}