using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Bonificacao { get; set; }
        public List<Pedido> Pedidos { get; set; }
    }
}