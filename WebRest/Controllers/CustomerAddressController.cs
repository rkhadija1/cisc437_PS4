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
    public class CustomerAddressController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public CustomerAddressController(WebRestOracleContext context)
        {
            _context = context;

        }

        // GET: 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerAddress>>> Get()
        {

            return await _context.CustomerAddresses.ToListAsync();
        }


        // GET: api/Customer
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CustomerAddress>> Get(string id)
        {
            var _customerAddress = await _context.CustomerAddresses.FindAsync(id);

            if (_customerAddress == null)
            {
                return NotFound();
            }

            return _customerAddress;
        }

        // PUT: api/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, CustomerAddress _item)
        {
            if (id != _item.CustomerAddressId)
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
        public async Task<ActionResult<CustomerAddress>> Post(CustomerAddress _item)
        {
            _context.CustomerAddresses.Add(_item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = _item.CustomerAddressId }, _item);
        }

        // DELETE: api//5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var _item = await _context.CustomerAddresses.FindAsync(id);
            if (_item == null)
            {
                return NotFound();
            }

            _context.CustomerAddresses.Remove(_item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.CustomerAddresses.Any(e => e.CustomerAddressId == id);
        }
        
    }
}
