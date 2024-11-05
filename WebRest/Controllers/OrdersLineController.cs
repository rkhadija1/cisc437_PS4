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
    public class OrdersLineController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public OrdersLineController(WebRestOracleContext context)
        {
            _context = context;

        }

        // GET: 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersLine>>> Get()
        {

            return await _context.OrdersLines.ToListAsync();
        }


        // GET: api/Customer
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<OrdersLine>> Get(string id)
        {
            var _ordersLine = await _context.OrdersLines.FindAsync(id);

            if (_ordersLine == null)
            {
                return NotFound();
            }

            return _ordersLine;
        }

        // PUT: api/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, OrdersLine _item)
        {
            if (id != _item.OrdersLineId)
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
        public async Task<ActionResult<OrdersLine>> Post(OrdersLine _item)
        {
            _context.OrdersLines.Add(_item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = _item.OrdersLineId }, _item);
        }

        // DELETE: api//5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var _item = await _context.OrdersLines.FindAsync(id);
            if (_item == null)
            {
                return NotFound();
            }

            _context.OrdersLines.Remove(_item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.OrdersLines.Any(e => e.OrdersLineId == id);
        }
        
    }
}
