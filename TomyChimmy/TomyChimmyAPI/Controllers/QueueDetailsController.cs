using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TomyChimmy.Models;
using TomyChimmyAPI.Areas.Identity.Data;

namespace TomyChimmyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueDetailsController : ControllerBase
    {
        private readonly TomyChimmyDbContext _context;

        public QueueDetailsController(TomyChimmyDbContext context)
        {
            _context = context;
        }

        // GET: api/QueueDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QueueDetail>>> GetQueueDetails()
        {
            return await _context.QueueDetails.ToListAsync();
        }

        // GET: api/QueueDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QueueDetail>> GetQueueDetail(int id)
        {
            var queueDetail = await _context.QueueDetails.FindAsync(id);

            if (queueDetail == null)
            {
                return NotFound();
            }

            return queueDetail;
        }

        // PUT: api/QueueDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQueueDetail(int id, QueueDetail queueDetail)
        {
            if (id != queueDetail.QueueDetail_ID)
            {
                return BadRequest();
            }

            _context.Entry(queueDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QueueDetailExists(id))
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

        // POST: api/QueueDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<QueueDetail>> PostQueueDetail(QueueDetail queueDetail)
        {
            _context.QueueDetails.Add(queueDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQueueDetail", new { id = queueDetail.QueueDetail_ID }, queueDetail);
        }

        // DELETE: api/QueueDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QueueDetail>> DeleteQueueDetail(int id)
        {
            var queueDetail = await _context.QueueDetails.FindAsync(id);
            if (queueDetail == null)
            {
                return NotFound();
            }

            _context.QueueDetails.Remove(queueDetail);
            await _context.SaveChangesAsync();

            return queueDetail;
        }

        private bool QueueDetailExists(int id)
        {
            return _context.QueueDetails.Any(e => e.QueueDetail_ID == id);
        }
    }
}
