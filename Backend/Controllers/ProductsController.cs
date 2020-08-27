using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Cors;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDb _context;

        public ProductsController(AppDb context)
        {
            _context = context;
        }

        // GET: api/Products
        [EnableCors("AllowOrigin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [EnableCors("AllowOrigin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);


            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [EnableCors("AllowOrigin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            //_context.Entry(product).State = EntityState.Modified;
            //var _product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

            //var detail = _product.Detail.FirstOrDefault(c => c == product.Detail.First());
            //detail = product.Detail.FirstOrDefault();

            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [EnableCors("AllowOrigin")]
        [HttpPost("{id}")]
        public async Task<ActionResult<Product>> PostProduct(int id, Detail detail)
        {
            var _product = await _context.Products.FindAsync(id);
            _product.Detail.Add(detail);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = detail.Id }, detail);
        }

        // DELETE: api/Products/5
        [EnableCors("AllowOrigin")]
        [HttpDelete("{id}/{detailId}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id, int detailId)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Detail.Remove(product.Detail.FirstOrDefault(m => m.Id == detailId));
            //_context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
