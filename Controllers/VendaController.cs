using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PottencialPaymentAPI.Context;
using PottencialPaymentAPI.Models;

namespace PottencialPaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly PaymentContext _context;

        public VendaController(PaymentContext context)
        {
            _context = context;
        }

        [HttpPost("Nova Venda")]
        public IActionResult Vender(NovaVenda novaVenda)
        {

            if (novaVenda == null) { return BadRequest(); }

            var nVenda = new Venda();

            nVenda.Produto = _context.Produtos.Where(x => x.IdProduto == novaVenda.IdProduto).FirstOrDefault();
            nVenda.Vendedor = _context.Vendedores.Where(x => x.IdVendedor == novaVenda.IDVendedor).FirstOrDefault();

            nVenda.StatusVenda = EnumStatusVenda.AguardandoPagamento.ToString();

            _context.Vendas.Add(nVenda);
            _context.SaveChanges();

            return Ok(nVenda);
        }

        [HttpPut("Atualizar Status")]
        public IActionResult AttVenda(int id, EnumStatusVenda statusVenda)
        {
            var venda = _context.Vendas.Where(x => x.IdVenda == id).FirstOrDefault();

            if (venda == null) { return BadRequest(); }

             venda.Produto = _context.Produtos.Where(x => x.IdProduto == venda.ProdutoId).FirstOrDefault();
             venda.Vendedor = _context.Vendedores.Where(x => x.IdVendedor == venda.VendedorId).FirstOrDefault();

            if (venda.StatusVenda == "AguardandoPagamento")
            {
                if (statusVenda.ToString() == "PagamentoAprovado" || statusVenda.ToString() == "Cancelada")
                {
                    venda.StatusVenda = statusVenda.ToString();
                    _context.Vendas.Update(venda);
                    _context.SaveChanges();
                    return Ok(venda);
                }
                else
                {
                    return BadRequest("transição invalida");
                }
            }

            if (venda.StatusVenda == "PagamentoAprovado" )
            {
                if (statusVenda.ToString() == "EnviadoParaTransportadora" || statusVenda.ToString() == "Cancelada")
                {
                    venda.StatusVenda = statusVenda.ToString();
                    _context.Vendas.Update(venda);
                    _context.SaveChanges();
                    return Ok(venda);
                }
                else
                {
                    return BadRequest("transição invalida");
                }
            }

            if (venda.StatusVenda == "EnviadoParaTransportadora")
            {
                if (statusVenda.ToString() == "Entregue")
                {
                    venda.StatusVenda = statusVenda.ToString();
                    _context.Vendas.Update(venda);
                    _context.SaveChanges();
                    return Ok(venda);
                }
                else
                {
                    return BadRequest("transição invalida");
                }
            }


            return BadRequest("não é possivel alterar o status atual");

        }

        [HttpGet("Pesquisar venda por ID")]
        public IActionResult VerVendas(int id)
        {
            var vendas = _context.Vendas.Where(x => x.IdVenda == id).FirstOrDefault();

            if (vendas == null) { return BadRequest(); }

            vendas.Vendedor = _context.Vendedores.Where(x => x.IdVendedor == vendas.VendedorId).FirstOrDefault();
            vendas.Produto = _context.Produtos.Where(x => x.IdProduto == vendas.ProdutoId).FirstOrDefault();

            return Ok(vendas);
        }

    }
}
