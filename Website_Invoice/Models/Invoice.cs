using System.ComponentModel.DataAnnotations;

namespace Website_Invoice.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        [Required(ErrorMessage = "Please select a customer")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Invoice date is required")]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }

        [Required(ErrorMessage = "Product total is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Product total must be between 0.01 and 999999.99")]
        public decimal ProductTotal { get; set; }

        [Required(ErrorMessage = "Sales tax is required")]
        [Range(0, 999999.99, ErrorMessage = "Sales tax must be between 0 and 999999.99")]
        public decimal SalesTax { get; set; }

        [Required(ErrorMessage = "Shipping is required")]
        [Range(0, 999999.99, ErrorMessage = "Shipping must be between 0 and 999999.99")]
        public decimal Shipping { get; set; }

        [Required(ErrorMessage = "Invoice total is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Invoice total must be between 0.01 and 999999.99")]
        public decimal InvoiceTotal { get; set; }

        public Customer? Customer { get; set; }

        [Timestamp]
        public byte[]? Timestamp { get; set; }
    }
}