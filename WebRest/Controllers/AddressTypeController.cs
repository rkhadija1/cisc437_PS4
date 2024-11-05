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
    public class AddressTypeController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public AddressTypeController(WebRestOracleContext context)
        {
            _context = context;

        }

        // GET: 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressType>>> Get()
        {

            return await _context.AddressTypes.ToListAsync();
        }


        // GET: api/Customer
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AddressType>> Get(string id)
        {
            var _addressType = await _context.AddressTypes.FindAsync(id);

            if (_addressType == null)
            {
                return NotFound();
            }

            return _addressType;
        }

        // PUT: api/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, AddressType _item)
        {
            if (id != _item.AddressTypeId)
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
        public async Task<ActionResult<AddressType>> Post(AddressType _item)
        {
            _context.AddressTypes.Add(_item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = _item.AddressTypeId }, _item);
        }

        // DELETE: api//5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var _item = await _context.AddressTypes.FindAsync(id);
            if (_item == null)
            {
                return NotFound();
            }

            _context.AddressTypes.Remove(_item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.AddressTypes.Any(e => e.AddressTypeId == id);
        }
        
    }
}
