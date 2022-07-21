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
    public class InvoicesController : Controller
    {
        private readonly TomyChimmyDbContext _context;

        public InvoicesController(TomyChimmyDbContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var tomyChimmyDbContext = _context.Invoices.Include(i => i.PayingMethod).Include(i => i.User);
            return View(await tomyChimmyDbContext.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.PayingMethod)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Invoice_ID == id);
            if (invoice == null)
            {
                return NotFound();
            }
            var invoiceView = new InvoiceViewModel();
            var invoiceDetail = new InvoiceDetail();

            invoiceView.Invoice = await _context.Invoices
                .Include(i => i.PayingMethod)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Invoice_ID == id);
            var dataID = _context.InvoiceDetails.Include(od => od.Invoice).Include(od => od.Food).Where(od => od.Invoice_ID.Equals(id)).ToList();

            invoiceView.Articulos = dataID;

            ViewData["ID_Comidas"] = new SelectList(_context.Foods, "ID_Comidas", "Descripción", invoiceDetail.ID_Comidas);
            ViewData["Invoice_ID"] = new SelectList(_context.Invoices, "Invoice_ID", "Apellidos", invoiceDetail.Invoice_ID);
            return View(invoiceView);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewData["Method_Id"] = new SelectList(_context.PayingMethods, "Method_Id", "FormaDePago");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Invoice_ID,UserId,Method_Id,FechaFactura,Subtotal,ValorImpuesto,Total,Nombres,Apellidos,Dirección")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Method_Id"] = new SelectList(_context.PayingMethods, "Method_Id", "FormaDePago", invoice.Method_Id);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", invoice.UserId);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["Method_Id"] = new SelectList(_context.PayingMethods, "Method_Id", "FormaDePago", invoice.Method_Id);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", invoice.UserId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Invoice_ID,UserId,Method_Id,FechaFactura,Subtotal,ValorImpuesto,Total,Nombres,Apellidos,Dirección")] Invoice invoice)
        {
            if (id != invoice.Invoice_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Invoice_ID))
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
            ViewData["Method_Id"] = new SelectList(_context.PayingMethods, "Method_Id", "FormaDePago", invoice.Method_Id);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", invoice.UserId);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.PayingMethod)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Invoice_ID == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Invoice_ID == id);
        }

        public async Task<IActionResult> _AgregarComida([Bind("InvoiceDetail_ID,ID_Comidas,Cantidad,ValorUnitario,ValorTotal,Invoice_ID")] InvoiceDetail invoiceDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceDetail);

                int id = invoiceDetail.Invoice_ID;

                int ID_Comidas = invoiceDetail.ID_Comidas;

                Models.Food articulos = _context.Foods.Find(ID_Comidas);

                decimal preciou = articulos.PrecioUnitario;
                decimal cantidad = invoiceDetail.Cantidad;

                decimal preciot = cantidad * preciou;

                invoiceDetail.ValorUnitario = preciou;
                invoiceDetail.ValorTotal = preciot;

                await _context.SaveChangesAsync();
                Models.Invoice invoice = _context.Invoices.Find(id);
                invoice.Subtotal += preciot;
                invoice.ValorImpuesto += Math.Round(Convert.ToDecimal(((double)preciot) * 0.18), 2);
                invoice.Total += preciot;
                _context.Update(articulos);
                _context.SaveChanges();


                return RedirectToAction("Details", new { id = id });
            }
            ViewData["ID_Comidas"] = new SelectList(_context.Foods, "ID_Comidas", "Descripción", invoiceDetail.ID_Comidas);
            ViewData["Invoice_ID"] = new SelectList(_context.Invoices, "Invoice_ID", "Apellidos", invoiceDetail.Invoice_ID);
            return View(invoiceDetail);
        }

        public async Task<IActionResult> InvoicePDF(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.PayingMethod)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Invoice_ID == id);
            if (invoice == null)
            {
                return NotFound();
            }
            var InvoiceViewModel = new InvoiceViewModel();
            var invoiceDetail = new InvoiceDetail();

            InvoiceViewModel.Invoice = await _context.Invoices
                .Include(i => i.PayingMethod)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Invoice_ID == id);
            var dataID = _context.InvoiceDetails.Include(qd => qd.Invoice).Include(qd => qd.Food).Where(qd => qd.Invoice_ID.Equals(id)).ToList();

            InvoiceViewModel.Articulos = dataID;

            ViewData["PayingMethod"] = new SelectList(_context.Invoices, "PayingMethod", "PayingMethod", invoiceDetail.Invoice_ID);
            ViewData["ID_Comidas"] = new SelectList(_context.Foods, "ID_Comidas", "Descripción", invoiceDetail.ID_Comidas);
            ViewData["Invoice_ID"] = new SelectList(_context.Invoices, "Invoice_ID", "Invoice_ID", invoiceDetail.Invoice_ID);

            return View(InvoiceViewModel);
        }
    }
}
