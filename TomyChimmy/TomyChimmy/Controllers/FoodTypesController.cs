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

    [Authorize(Roles = "Manager")]
    public class FoodTypesController : Controller
    {
        private readonly TomyChimmyDbContext _context;

        public FoodTypesController(TomyChimmyDbContext context)
        {
            _context = context;
        }

        // GET: FoodTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.FoodTypes.ToListAsync());
        }

        // GET: FoodTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodType = await _context.FoodTypes
                .FirstOrDefaultAsync(m => m.FoodType_ID == id);
            if (foodType == null)
            {
                return NotFound();
            }

            return View(foodType);
        }

        // GET: FoodTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodType_ID,Detalle")] FoodType foodType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodType);
        }

        // GET: FoodTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodType = await _context.FoodTypes.FindAsync(id);
            if (foodType == null)
            {
                return NotFound();
            }
            return View(foodType);
        }

        // POST: FoodTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodType_ID,Detalle")] FoodType foodType)
        {
            if (id != foodType.FoodType_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodTypeExists(foodType.FoodType_ID))
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
            return View(foodType);
        }

        // GET: FoodTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodType = await _context.FoodTypes
                .FirstOrDefaultAsync(m => m.FoodType_ID == id);
            if (foodType == null)
            {
                return NotFound();
            }

            return View(foodType);
        }

        // POST: FoodTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodType = await _context.FoodTypes.FindAsync(id);
            _context.FoodTypes.Remove(foodType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodTypeExists(int id)
        {
            return _context.FoodTypes.Any(e => e.FoodType_ID == id);
        }
    }
}
