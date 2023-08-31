using Iseseisvus_Töö_ORM_Gužov.Data;
using Iseseisvus_Töö_ORM_Gužov.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Iseseisvus_Töö_ORM_Gužov.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartProductController : ControllerBase
    {
        //base
        private readonly ApplicationDbContext _context;

        public CartProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        //create
        [HttpPost]
        public async Task<IActionResult> CreateCartProduct(CartProduct cartProduct)
        {
            _context.CartProducts.Add(cartProduct);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCartProduct), new { id = cartProduct.Id }, cartProduct);
        }
        //get/read
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartProduct(int id)
        {
            var cartProduct = await _context.CartProducts.FindAsync(id);

            if (cartProduct == null)
                return NotFound();

            return Ok(cartProduct);
        }
        //update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartProduct(int id, CartProduct updatedCartProduct)
        {
            if (id != updatedCartProduct.Id)
                return BadRequest();

            _context.Entry(updatedCartProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartProductExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }
        //delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartProduct(int id)
        {
            var cartProduct = await _context.CartProducts.FindAsync(id);

            if (cartProduct == null)
                return NotFound();

            _context.CartProducts.Remove(cartProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //check
        private bool CartProductExists(int id)
        {
            return _context.CartProducts.Any(cp => cp.Id == id);
        }
    }

}
