using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TommyAPI.Data;
using TommyAPI.Models;

namespace TommyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayingMethodsController : ControllerBase
    {
        private readonly TommyAPIContext _context;

        public PayingMethodsController(TommyAPIContext context)
        {
            _context = context;
        }

        // GET: api/PayingMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayingMethod>>> GetPayingMethods()
        {
            return await _context.PayingMethods.ToListAsync();
        }

        // GET: api/PayingMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayingMethod>> GetPayingMethod(int id)
        {
            var payingMethod = await _context.PayingMethods.FindAsync(id);

            if (payingMethod == null)
            {
                return NotFound();
            }

            return payingMethod;
        }

        // PUT: api/PayingMethods/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayingMethod(int id, PayingMethod payingMethod)
        {
            if (id != payingMethod.Method_Id)
            {
                return BadRequest();
            }

            _context.Entry(payingMethod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayingMethodExists(id))
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

        // POST: api/PayingMethods
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PayingMethod>> PostPayingMethod(PayingMethod payingMethod)
        {
            _context.PayingMethods.Add(payingMethod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayingMethod", new { id = payingMethod.Method_Id }, payingMethod);
        }

        // DELETE: api/PayingMethods/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PayingMethod>> DeletePayingMethod(int id)
        {
            var payingMethod = await _context.PayingMethods.FindAsync(id);
            if (payingMethod == null)
            {
                return NotFound();
            }

            _context.PayingMethods.Remove(payingMethod);
            await _context.SaveChangesAsync();

            return payingMethod;
        }

        private bool PayingMethodExists(int id)
        {
            return _context.PayingMethods.Any(e => e.Method_Id == id);
        }
    }
}
