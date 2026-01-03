using System.ComponentModel.DataAnnotations;

namespace Website_Invoice.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal ProductTotal { get; set; }
        public decimal SalesTax { get; set; }
        public decimal Shipping { get; set; }
        public decimal InvoiceTotal { get; set; }

        public Customer? Customer { get; set; }

        [Timestamp]
        public byte[]? Timestamp { get; set; }
    }
}
