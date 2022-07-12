using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public double ValorTotal { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }
        public int PagamentoId { get; set; }
        public Pagamento Pagamento { get; set; }
        public List<Produto> Produtos { get; set; }

    }
}