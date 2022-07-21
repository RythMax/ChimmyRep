using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TomyChimmy.Data;
using TomyChimmy.Models;
using TomyChimmy.ViewModels;

namespace TomyChimmy.Controllers
{
    public class OrdersController : Controller
    {
        private readonly TomyChimmyDbContext _context;

        public OrdersController(TomyChimmyDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.ID_Orden == id);
            if (order == null)
            {
                return NotFound();
            }
            var OrderViewModel = new OrderViewModel();
            var orderDetail = new OrderDetail();

            OrderViewModel.Order = await _context.Orders
                .FirstOrDefaultAsync(m => m.ID_Orden == id);
            var dataOD = _context.OrderDetails.Include(qd => qd.Order).Include(qd => qd.Food).Where(qd => qd.ID_Orden.Equals(id)).ToList();

            OrderViewModel.Artículos = dataOD;

            ViewData["ID_Comidas"] = new SelectList(_context.Foods, "ID_Comidas", "Descripción", orderDetail.ID_Comidas);
            ViewData["ID_Orden"] = new SelectList(_context.Orders, "ID_Orden", "Anotaciones", orderDetail.ID_Orden);
            return View(OrderViewModel);

        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Orden,DateOrden,Subtotal,ValorImpuesto,Total,Anotaciones")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Orden,DateOrden,Subtotal,ValorImpuesto,Total,Anotaciones")] Order order)
        {
            if (id != order.ID_Orden)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.ID_Orden))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.ID_Orden == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID_Orden == id);
        }

        public async Task<IActionResult> _AgregarProductos([Bind("OrderDetailID,ID_Comidas,CantidadDeArticulos,ValorUnitario,ValorTotal,ID_Orden")] OrderDetail orderDetail)
        {

            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);

                int id = orderDetail.ID_Orden;
                int id_food = orderDetail.ID_Comidas;

                Food articulos = _context.Foods.Find(id_food);

                decimal preciou = articulos.PrecioUnitario;
                decimal cantidad = orderDetail.CantidadDeArticulos;

                decimal preciot = cantidad * preciou;

                orderDetail.ValorUnitario = preciou;
                orderDetail.ValorTotal = preciot;

                await _context.SaveChangesAsync();
                Order order = _context.Orders.Find(id);
                order.Subtotal += preciot;
                order.ValorImpuesto += Math.Round(Convert.ToDecimal(((double)preciot) * 0.18), 2);
                order.Total += preciot;
                articulos.Cantidad += orderDetail.CantidadDeArticulos;
                _context.Update(order);
                _context.SaveChanges();

                return RedirectToAction("Details", new {id = id});
            }
            ViewData["ID_Comidas"] = new SelectList(_context.Foods, "ID_Comidas", "Descripción", orderDetail.ID_Comidas);
            ViewData["ID_Orden"] = new SelectList(_context.Orders, "ID_Orden", "Anotaciones", orderDetail.ID_Orden);
            return View(orderDetail);

        }

        public async Task<IActionResult> OrderPDF(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queue = await _context.Orders
                .FirstOrDefaultAsync(m => m.ID_Orden == id);
            if (queue == null)
            {
                return NotFound();
            }
            var OrderViewModel = new OrderViewModel();
            var orderDetail = new OrderDetail();

            OrderViewModel.Order = await _context.Orders
                .FirstOrDefaultAsync(m => m.ID_Orden == id);
            var dataOD = _context.OrderDetails.Include(qd => qd.Order).Include(qd => qd.Food).Where(qd => qd.ID_Orden.Equals(id)).ToList();

            OrderViewModel.Artículos = dataOD;

            ViewData["ID_Comidas"] = new SelectList(_context.Foods, "ID_Comidas", "Descripción", orderDetail.ID_Comidas);
            ViewData["ID_Orden"] = new SelectList(_context.Orders, "ID_Orden", "Anotaciones", orderDetail.ID_Orden);
            return View(OrderViewModel);
        }

    }
}

