using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRestEF.EF.Data;
using WebRestEF.EF.Models;

namespace WebRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStatusController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public ProductStatusController(WebRestOracleContext context)
        {
            _context = context;

        }

        // GET: 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStatus>>> Get()
        {

            return await _context.ProductStatuses.ToListAsync();
        }


        // GET: api/Customer
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductStatus>> Get(string id)
        {
            var _productStatus = await _context.ProductStatuses.FindAsync(id);

            if (_productStatus == null)
            {
                return NotFound();
            }

            return _productStatus;
        }

        // PUT: api/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, ProductStatus _item)
        {
            if (id != _item.ProductStatusId)
            {
                return BadRequest();
            }

            _context.Entry(_item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
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

        // POST: api/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductStatus>> Post(ProductStatus _item)
        {
            _context.ProductStatuses.Add(_item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = _item.ProductStatusId }, _item);
        }

        // DELETE: api//5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var _item = await _context.ProductStatuses.FindAsync(id);
            if (_item == null)
            {
                return NotFound();
            }

            _context.ProductStatuses.Remove(_item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.ProductStatuses.Any(e => e.ProductStatusId == id);
        }
        
    }
}
