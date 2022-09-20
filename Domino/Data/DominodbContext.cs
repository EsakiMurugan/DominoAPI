using Domino.Model;
using Microsoft.EntityFrameworkCore;

namespace Domino.Data
{
    public class DominodbContext : DbContext
    {
        public DominodbContext() { }
        public DominodbContext(DbContextOptions options): base(options) { }
        public DbSet<Pizza> pizza { get; set; }
        public DbSet<Cart> cart { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<Customer> customers { get; set; }  
        public DbSet<Receipt> receipts { get; set; }
        public DbSet<Admin> admin { get; set; } 

    }
}
