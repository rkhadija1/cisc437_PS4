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
    public class CustomerController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public CustomerController(WebRestOracleContext context)
        {
            _context = context;

        }

        // GET: 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {

            return await _context.Customers.ToListAsync();
        }


        // GET: api/Customer
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Customer>> Get(string id)
        {
            var _customer = await _context.Customers.FindAsync(id);

            if (_customer == null)
            {
                return NotFound();
            }

            return _customer;
        }

        // PUT: api/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Customer _customer)
        {
            if (id != _customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(_customer).State = EntityState.Modified;

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
        public async Task<ActionResult<Customer>> Post(Customer _customer)
        {
            _context.Customers.Add(_customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = _customer.CustomerId }, _customer);
        }

        // DELETE: api//5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var _customer = await _context.Customers.FindAsync(id);
            if (_customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(_customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
        
    }
}
