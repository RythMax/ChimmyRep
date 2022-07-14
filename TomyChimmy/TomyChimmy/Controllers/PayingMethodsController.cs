using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TomyChimmy.Data;
using TomyChimmy.Models;

namespace TomyChimmy.Controllers
{
    [Authorize (Roles = "Manager")]
    public class PayingMethodsController : Controller
    {
        private readonly TomyChimmyDbContext _context;

        public PayingMethodsController(TomyChimmyDbContext context)
        {
            _context = context;
        }

        // GET: PayingMethods
        public async Task<IActionResult> Index()
        {
            return View(await _context.PayingMethods.ToListAsync());
        }

        // GET: PayingMethods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payingMethod = await _context.PayingMethods
                .FirstOrDefaultAsync(m => m.Method_Id == id);
            if (payingMethod == null)
            {
                return NotFound();
            }

            return View(payingMethod);
        }

        // GET: PayingMethods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PayingMethods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Method_Id,FormaDePago")] PayingMethod payingMethod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payingMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payingMethod);
        }

        // GET: PayingMethods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payingMethod = await _context.PayingMethods.FindAsync(id);
            if (payingMethod == null)
            {
                return NotFound();
            }
            return View(payingMethod);
        }

        // POST: PayingMethods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Method_Id,FormaDePago")] PayingMethod payingMethod)
        {
            if (id != payingMethod.Method_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payingMethod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayingMethodExists(payingMethod.Method_Id))
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
            return View(payingMethod);
        }

        // GET: PayingMethods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payingMethod = await _context.PayingMethods
                .FirstOrDefaultAsync(m => m.Method_Id == id);
            if (payingMethod == null)
            {
                return NotFound();
            }

            return View(payingMethod);
        }

        // POST: PayingMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payingMethod = await _context.PayingMethods.FindAsync(id);
            _context.PayingMethods.Remove(payingMethod);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayingMethodExists(int id)
        {
            return _context.PayingMethods.Any(e => e.Method_Id == id);
        }
    }
}
