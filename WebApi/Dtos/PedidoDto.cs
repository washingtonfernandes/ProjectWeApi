using Domain.Entities;

namespace WebApi.Dtos
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public double ValorTotal { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
        public int PagamentoId { get; set; }
    }
}