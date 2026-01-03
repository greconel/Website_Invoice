using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace Website_Invoice.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        public List<Invoice>? Invoices { get; set; }

        [Timestamp]
        public byte[]? Timestamp { get; set; }
    }
}
