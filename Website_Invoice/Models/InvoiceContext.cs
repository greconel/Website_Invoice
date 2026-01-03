using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Website_Invoice.Models
{
    public class InvoiceContext : IdentityDbContext
    {
        public InvoiceContext(DbContextOptions<InvoiceContext> options)
            : base(options) { }
    }
}
