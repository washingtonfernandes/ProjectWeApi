using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace WebApi.Dtos
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public int? QuantVenda { get; set; }
        public int Quantidade { get; set; }
        public int CategoriaId { get; set; }

    }
}