using CBozal.Api.Ecommerce.DataBase;
using CBozal.Api.Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBozal.Api.Ecommerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ContextDb _contextDb;

        public ProductController(ContextDb contextDb)
        {
            _contextDb = contextDb;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            if (_contextDb.Product.Any())
                return Ok(_contextDb.Product);
            else
                return NoContent();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public ActionResult GetProductById(int id)
        {
            if (_contextDb.Product.Any(p => p.Id == id))
                return Ok(_contextDb.Product.FirstOrDefault(p => p.Id == id));
            else
                return NoContent();
        }

        [HttpGet("category/{id}")]
        public ActionResult<IEnumerable<Product>> GetProductsByCategoryId(int id)
        {
            if (_contextDb.Product.Any(c => c.CategoryId == id))
                return Ok(_contextDb.Product.Where(c => c.Id == id));
            else
                return NoContent();
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Add([FromBody] Product product)
        {
            if (!_contextDb.Product.Any(p => p.Id == product.Id))
            {
                _contextDb.Product.Add(product);
                _contextDb.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"Ya existe con producto con el id de {product.Id}");
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            if (_contextDb.Product.Any(p => p.Id == id))
            {
                var ProductToUpdate = _contextDb.Product.Single(p => p.Id == id);
                _contextDb.Product.Remove(ProductToUpdate);
                _contextDb.Product.Add(product);
                _contextDb.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"No existe el producto con id {id}");
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_contextDb.Product.Any(p => p.Id == id))
            {
                var ProductToDelete = _contextDb.Product.Single(p => p.Id == id);
                _contextDb.Product.Remove(ProductToDelete);
                _contextDb.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"No existe el producto con id {id}");
            }
        }
    }
}
