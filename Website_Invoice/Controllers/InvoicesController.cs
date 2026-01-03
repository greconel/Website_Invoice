using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Website_Invoice.Models;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Website_Invoice.Controllers
{
    //Stap 4
    public class InvoicesController : Controller
    {
        private readonly IAdminRepository<Invoice> _invoiceRepository;
        private readonly IAdminRepository<Customer> _customerRepository;

        public InvoicesController(
            IAdminRepository<Invoice> invoiceRepository,
            IAdminRepository<Customer> customerRepository)
        {
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            return View(await _invoiceRepository.GetListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _invoiceRepository.GetAsync(id.Value);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var customers = await _customerRepository.GetListAsync();
            ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "Name");
            return View();
        }

        // POST: Invoices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("InvoiceId,CustomerId,InvoiceDate,ProductTotal,SalesTax,Shipping,InvoiceTotal")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                await _invoiceRepository.AddAsync(invoice);
                return RedirectToAction(nameof(Index));
            }
            var customers = await _customerRepository.GetListAsync();
            ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "Name", invoice.CustomerId);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _invoiceRepository.GetAsync(id.Value);
            if (invoice == null)
            {
                return NotFound();
            }
            var customers = await _customerRepository.GetListAsync();
            ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "Name", invoice.CustomerId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("InvoiceId,CustomerId,InvoiceDate,ProductTotal,SalesTax,Shipping,InvoiceTotal,Timestamp")] Invoice invoice)
        {
            if (id != invoice.InvoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                HttpStatusCode statusCode = await _invoiceRepository.UpdateAsync(id, invoice);

                if (statusCode == HttpStatusCode.BadRequest || statusCode == HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                else if (statusCode == HttpStatusCode.Conflict)
                {
                    ModelState.AddModelError("", "Unable to update - this invoice was modified by another user.");
                    ViewData["CustomerId"] = new SelectList(await _customerRepository.GetListAsync(), "CustomerId", "Name", invoice.CustomerId);
                    return View(invoice);
                }

                return RedirectToAction(nameof(Index));
            }
            var customers = await _customerRepository.GetListAsync();
            ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "Name", invoice.CustomerId);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _invoiceRepository.GetAsync(id.Value);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _invoiceRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}