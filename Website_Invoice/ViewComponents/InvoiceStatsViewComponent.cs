using Microsoft.AspNetCore.Mvc;
using Website_Invoice.Models;

namespace Website_Invoice.ViewComponents
{
    public class InvoiceStatsViewComponent : ViewComponent
    {
        private readonly IAdminRepository<Invoice> _invoiceRepository;

        public InvoiceStatsViewComponent(IAdminRepository<Invoice> invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var invoices = await _invoiceRepository.GetListAsync();

            var stats = new InvoiceStats
            {
                TotalInvoices = invoices?.Count() ?? 0,
                TotalAmount = invoices?.Sum(i => i.InvoiceTotal) ?? 0,
                AverageAmount = invoices?.Any() == true ? invoices.Average(i => i.InvoiceTotal) : 0
            };

            return View(stats);
        }
    }

    public class InvoiceStats
    {
        public int TotalInvoices { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AverageAmount { get; set; }
    }
}