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
    public class OrderDetailsController : Controller
    {
        private readonly TomyChimmyDbContext _context;

        public OrderDetailsController(TomyChimmyDbContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var tomyChimmyDbContext = _context.OrderDetails.Include(o => o.Food).Include(o => o.Order);
            return View(await tomyChimmyDbContext.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Food)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.OrderDetailID == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            ViewData["ID_Comidas"] = new SelectList(_context.Foods, "ID_Comidas", "Descripción");
            ViewData["ID_Orden"] = new SelectList(_context.Orders, "ID_Orden", "Anotaciones");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDetailID,ID_Comidas,CantidadDeArticulos,ValorUnitario,ValorTotal,ID_Orden")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);

                int id = orderDetail.ID_Orden;
                int id_food = orderDetail.ID_Comidas;

                Food food = _context.Foods.Find(id_food);
                decimal preciou = orderDetail.Food.PrecioUnitario;
                decimal cantidad = orderDetail.CantidadDeArticulos;
                decimal preciot = preciou * cantidad;

                orderDetail.ValorUnitario = preciou;
                orderDetail.ValorTotal = preciot;

                await _context.SaveChangesAsync();

                Order order = _context.Orders.Find(id);
                order.Subtotal += preciot;
                order.ValorImpuesto += Math.Round(Convert.ToDecimal(((double)preciot) * 0.18), 2);
                order.Total += preciot;
                _context.Update(order);
                _context.SaveChanges();
                return View(orderDetail);
            }
            ViewData["ID_Comidas"] = new SelectList(_context.Foods, "ID_Comidas", "Descripción", orderDetail.ID_Comidas);
            ViewData["ID_Orden"] = new SelectList(_context.Orders, "ID_Orden", "Anotaciones", orderDetail.ID_Orden);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            ViewData["ID_Comidas"] = new SelectList(_context.Foods, "ID_Comidas", "Descripción", orderDetail.ID_Comidas);
            ViewData["ID_Orden"] = new SelectList(_context.Orders, "ID_Orden", "Anotaciones", orderDetail.ID_Orden);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDetailID,ID_Comidas,CantidadDeArticulos,ValorUnitario,ValorTotal,ID_Orden")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrderDetailID))
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
            ViewData["ID_Comidas"] = new SelectList(_context.Foods, "ID_Comidas", "Descripción", orderDetail.ID_Comidas);
            ViewData["ID_Orden"] = new SelectList(_context.Orders, "ID_Orden", "Anotaciones", orderDetail.ID_Orden);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Food)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.OrderDetailID == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrderDetailID == id);
        }
    }
}
