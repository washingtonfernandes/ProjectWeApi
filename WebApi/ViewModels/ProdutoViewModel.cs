using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModels
{
    public class ProdutoViewModel
    {
        public string Nome { get; set; }
        public double Preco { get; set; }
        public int? QuantVenda { get; set; }
        public int Quantidade { get; set; }
        public int CategoriaId { get; set; }

    }
}