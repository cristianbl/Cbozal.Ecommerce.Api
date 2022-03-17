using CBozal.Api.Ecommerce.DataBase;
using CBozal.Api.Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBozal.Api.Ecommerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopcartController : ControllerBase
    {
        private readonly ContextDb _contextDb;
        public ShopcartController(ContextDb contextDb)
        {
            _contextDb = contextDb;
        }
        // GET: api/<ShopcartController>
        [HttpGet]
        public ActionResult<IEnumerable<Shopcart>> GetCart()
        {
            if (_contextDb.Shopcart.Any())
                return Ok(_contextDb.Shopcart.Include(x => x.Product));
            else
                return NoContent();
        }

        // GET api/<ShopcartController>/5
        [HttpGet("{email}")]
        public ActionResult<IEnumerable<Shopcart>> GetCartByEmail(String email)
        {
            if (_contextDb.Shopcart.Any(x => x.Email.ToLower().Trim() == email.ToLower().Trim()))
                return Ok(_contextDb.Shopcart.Include(x => x.Product).Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim()));
            else
                return NoContent();
        }

        // POST api/<ShopcartController>
        [HttpPost]
        public IActionResult Add([FromBody] Shopcart shopcart)
        {
            if (!_contextDb.Shopcart.Any(x => x.Id == shopcart.Id))
            {
                _contextDb.Shopcart.Add(shopcart);
                _contextDb.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"Existe la cesta con el id de {shopcart.Id}");
            }
        }

        // PUT api/<ShopcartController>/5
        [HttpPut("{email}/{idProduct}")]
        public IActionResult Update(String email, int idProduct, [FromBody] int cantidad)
        {
            if (_contextDb.Shopcart.Any(x => x.Email.ToLower().Trim() == email.ToLower().Trim() && x.Id == idProduct))
            {
                var CartToUpdate = _contextDb.Shopcart.Single(x => x.Email.ToLower().Trim() == email.ToLower().Trim() && x.Id == idProduct);
                CartToUpdate.Quantity = cantidad;
                _contextDb.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"No existe la cesta con el correo {email} ay el producto-id {idProduct}");
            }
        }

        // DELETE api/<ShopcartController>/5
        [HttpDelete("{email}")]
        public IActionResult DeleteCarts(String email)
        {
            if (_contextDb.Shopcart.Any(x => x.Email.ToLower().Trim() == email.ToLower().Trim()))
            {
                var CartsToDelete = _contextDb.Shopcart.Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim());
                _contextDb.Shopcart.RemoveRange(CartsToDelete);
                _contextDb.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"No existe ninguna cesta con el correo {email}");
            }
        }

        // DELETE api/<CartsController>/email/idProduct
        [HttpDelete("{email}/{idProduct}")]
        public IActionResult DeleteCart(String email, int idProduct)
        {
            if (_contextDb.Shopcart.Any(x => x.Email.ToLower().Trim() == email.ToLower().Trim() && x.Id == idProduct))
            {
                var CartToDelete = _contextDb.Shopcart.Single(x => x.Email.ToLower().Trim() == email.ToLower().Trim() && x.Id == idProduct);
                _contextDb.Shopcart.Remove(CartToDelete);
                _contextDb.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"No existe la cesta con el correo {email} ay el producto-id {idProduct}");
            }
        }
    }
}
