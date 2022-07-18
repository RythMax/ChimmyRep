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
    public class QueueDetailsController : Controller
    {
        private readonly TomyChimmyDbContext _context;

        public QueueDetailsController(TomyChimmyDbContext context)
        {
            _context = context;
        }

        // GET: QueueDetails
        public async Task<IActionResult> Index()
        {
            var tomyChimmyDbContext = _context.QueueDetails.Include(q => q.Food).Include(q => q.Queue);
            return View(await tomyChimmyDbContext.ToListAsync());
        }

        // GET: QueueDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queueDetail = await _context.QueueDetails
                .Include(q => q.Food)
                .Include(q => q.Queue)
                .FirstOrDefaultAsync(m => m.QueueDetail_ID == id);
            if (queueDetail == null)
            {
                return NotFound();
            }

            return View(queueDetail);
        }

        // GET: QueueDetails/Create
        public IActionResult Create()
        {
            ViewData["ID_Comidas"] = new SelectList(_context.Foods, "ID_Comidas", "Descripción");
            ViewData["Pedido_ID"] = new SelectList(_context.Queues, "Pedido_ID", "Apellidos");
            return View();
        }

        // POST: QueueDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QueueDetail_ID,ID_Comidas,Cantidad,ValorUnitario,ValorTotal,Pedido_ID")] QueueDetail queueDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(queueDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID_Comidas"] = new SelectList(_context.Foods, "ID_Comidas", "Descripción", queueDetail.ID_Comidas);
            ViewData["Pedido_ID"] = new SelectList(_context.Queues, "Pedido_ID", "Apellidos", queueDetail.Pedido_ID);
            return View(queueDetail);
        }

        // GET: QueueDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queueDetail = await _context.QueueDetails.FindAsync(id);
            if (queueDetail == null)
            {
                return NotFound();
            }
            ViewData["ID_Comidas"] = new SelectList(_context.Foods, "ID_Comidas", "Descripción", queueDetail.ID_Comidas);
            ViewData["Pedido_ID"] = new SelectList(_context.Queues, "Pedido_ID", "Apellidos", queueDetail.Pedido_ID);
            return View(queueDetail);
        }

        // POST: QueueDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QueueDetail_ID,ID_Comidas,Cantidad,ValorUnitario,ValorTotal,Pedido_ID")] QueueDetail queueDetail)
        {
            if (id != queueDetail.QueueDetail_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(queueDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QueueDetailExists(queueDetail.QueueDetail_ID))
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
            ViewData["ID_Comidas"] = new SelectList(_context.Foods, "ID_Comidas", "Descripción", queueDetail.ID_Comidas);
            ViewData["Pedido_ID"] = new SelectList(_context.Queues, "Pedido_ID", "Apellidos", queueDetail.Pedido_ID);
            return View(queueDetail);
        }

        // GET: QueueDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queueDetail = await _context.QueueDetails
                .Include(q => q.Food)
                .Include(q => q.Queue)
                .FirstOrDefaultAsync(m => m.QueueDetail_ID == id);
            if (queueDetail == null)
            {
                return NotFound();
            }

            return View(queueDetail);
        }

        // POST: QueueDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var queueDetail = await _context.QueueDetails.FindAsync(id);
            _context.QueueDetails.Remove(queueDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QueueDetailExists(int id)
        {
            return _context.QueueDetails.Any(e => e.QueueDetail_ID == id);
        }
    }
}
