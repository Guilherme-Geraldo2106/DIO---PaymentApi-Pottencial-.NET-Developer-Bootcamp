
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PottencialPaymentAPI.Models
{
    public class Venda 
    {
        [Key]
        public int IdVenda { get; set; }
        public string StatusVenda { get; set; }
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }
        public int VendedorId { get; set; }
        public virtual Vendedor Vendedor { get; set; }
    }
}
