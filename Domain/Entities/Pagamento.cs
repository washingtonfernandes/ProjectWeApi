using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pagamento
    {
    public int Id { get; set; }
    public string Tipo { get; set; }
    public List<Pedido> Pedidos { get; set; }
    }
}