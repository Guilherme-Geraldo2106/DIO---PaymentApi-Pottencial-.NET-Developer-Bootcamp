using Microsoft.EntityFrameworkCore;
using PottencialPaymentAPI.Models;

namespace PottencialPaymentAPI.Context
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Produto> Produtos  { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }

    }
}
