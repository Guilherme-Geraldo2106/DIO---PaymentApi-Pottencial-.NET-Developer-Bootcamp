
using System.ComponentModel.DataAnnotations;

namespace PottencialPaymentAPI.Models
{
    public  class Vendedor 
    {
        [Key]
        public int IdVendedor { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}