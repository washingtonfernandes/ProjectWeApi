using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModels
{
    public class PedidoViewModel
    {
        public DateTime DataPedido { get; set; }
        public double ValorTotal { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
        public int PagamentoId { get; set; }
        public List<InsertProdutoViewModel> InserirProdutos { get; set; }
    }
}