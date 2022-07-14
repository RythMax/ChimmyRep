using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TomyChimmy.Data;
using TomyChimmy.Models;

namespace TomyChimmy.Controllers
{
    public class QueuesController : Controller
    {
        private readonly TomyChimmyDbContext _context;

        public QueuesController(TomyChimmyDbContext context)
        {
            _context = context;
        }

        // GET: Queues
        public async Task<IActionResult> Index()
        {
            var tomyChimmyDbContext = _context.Queues.Include(q => q.PayingMethod).Include(q => q.Status);
            return View(await tomyChimmyDbContext.ToListAsync());
        }

        // GET: Queues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queue = await _context.Queues
                .Include(q => q.PayingMethod)
                .Include(q => q.Status)
                .FirstOrDefaultAsync(m => m.Pedido_ID == id);
            if (queue == null)
            {
                return NotFound();
            }

            return View(queue);
        }

        // GET: Queues/Create
        public IActionResult Create()
        {
            ViewData["Method_Id"] = new SelectList(_context.PayingMethods, "Method_Id", "FormaDePago");
            ViewData["Status_ID"] = new SelectList(_context.Statuses, "Status_ID", "Descripcion");
            return View();
        }

        // POST: Queues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pedido_ID,Id,DatePedido,Method_Id,Anotaciones,Subtotal,Valor_Impuesto,Total,Status_ID")] Queue queue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(queue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Method_Id"] = new SelectList(_context.PayingMethods, "Method_Id", "FormaDePago", queue.Method_Id);
            ViewData["Status_ID"] = new SelectList(_context.Statuses, "Status_ID", "Descripcion", queue.Status_ID);
            return View(queue);
        }

        // GET: Queues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queue = await _context.Queues.FindAsync(id);
            if (queue == null)
            {
                return NotFound();
            }
            ViewData["Method_Id"] = new SelectList(_context.PayingMethods, "Method_Id", "FormaDePago", queue.Method_Id);
            ViewData["Status_ID"] = new SelectList(_context.Statuses, "Status_ID", "Descripcion", queue.Status_ID);
            return View(queue);
        }

        // POST: Queues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Pedido_ID,Id,DatePedido,Method_Id,Anotaciones,Subtotal,Valor_Impuesto,Total,Status_ID")] Queue queue)
        {
            if (id != queue.Pedido_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(queue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QueueExists(queue.Pedido_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Method_Id"] = new SelectList(_context.PayingMethods, "Method_Id", "FormaDePago", queue.Method_Id);
            ViewData["Status_ID"] = new SelectList(_context.Statuses, "Status_ID", "Descripcion", queue.Status_ID);
            return View(queue);
        }

        // GET: Queues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queue = await _context.Queues
                .Include(q => q.PayingMethod)
                .Include(q => q.Status)
                .FirstOrDefaultAsync(m => m.Pedido_ID == id);
            if (queue == null)
            {
                return NotFound();
            }

            return View(queue);
        }

        // POST: Queues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var queue = await _context.Queues.FindAsync(id);
            _context.Queues.Remove(queue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QueueExists(int id)
        {
            return _context.Queues.Any(e => e.Pedido_ID == id);
        }
    }
}
