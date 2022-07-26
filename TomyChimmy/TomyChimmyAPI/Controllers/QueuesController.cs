﻿using System;
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
    public class QueuesController : ControllerBase
    {
        private readonly TomyChimmyDbContext _context;

        public QueuesController(TomyChimmyDbContext context)
        {
            _context = context;
        }

        // GET: api/Queues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Queue>>> GetQueues()
        {
            return await _context.Queues.ToListAsync();
        }

        // GET: api/Queues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Queue>> GetQueue(int id)
        {
            var queue = await _context.Queues.FindAsync(id);

            if (queue == null)
            {
                return NotFound();
            }

            return queue;
        }

        // PUT: api/Queues/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQueue(int id, Queue queue)
        {
            if (id != queue.Pedido_ID)
            {
                return BadRequest();
            }

            _context.Entry(queue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QueueExists(id))
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

        // POST: api/Queues
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Queue>> PostQueue(Queue queue)
        {
            _context.Queues.Add(queue);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQueue", new { id = queue.Pedido_ID }, queue);
        }

        // DELETE: api/Queues/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Queue>> DeleteQueue(int id)
        {
            var queue = await _context.Queues.FindAsync(id);
            if (queue == null)
            {
                return NotFound();
            }

            _context.Queues.Remove(queue);
            await _context.SaveChangesAsync();

            return queue;
        }

        private bool QueueExists(int id)
        {
            return _context.Queues.Any(e => e.Pedido_ID == id);
        }
    }
}
