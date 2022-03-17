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
    public class CategoryController : ControllerBase
    {
        private readonly ContextDb _contextDb;

        public CategoryController(ContextDb contextDb)
        {
            _contextDb = contextDb;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategory()
        {
            if (!_contextDb.Category.Any())
                return Ok(_contextDb.Category);
            else
                return NoContent();
            
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public ActionResult GetCategoryById(int id)
        {
            if (_contextDb.Category.Any(x => x.Id == id))
                return Ok(_contextDb.Category.FirstOrDefault(x => x.Id == id));
            else
                return NoContent();
        }

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult AddCategory([FromBody] Category category)
        {
            if (!_contextDb.Category.Any(x => x.Id == category.Id))
            {
                _contextDb.Category.Add(category);
                _contextDb.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"Ya existe la categoria: {category.Id}");
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            var CategoryToUpdate = _contextDb.Category.Single(x => x.Id == id);
            _contextDb.Category.Remove(CategoryToUpdate);
            _contextDb.Category.Add(category);
            _contextDb.SaveChanges();
            return Ok();
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var CategoryToDelete = _contextDb.Category.Single(x => x.Id == id);
            _contextDb.Category.Remove(CategoryToDelete);
            return Ok();
        }
    }
}
