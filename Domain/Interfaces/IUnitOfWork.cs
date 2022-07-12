using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        IClienteRepository ClienteRepository {get;}   
        IPedidoRepository PedidoRepository {get;}   
        IVendedorRepository VendedorRepository {get;}   
        IProdutoRepository ProdutoRepository {get;}   
        IPagamentoRepository PagamentoRepository {get;}   
        ICategoriaRepository CategoriaRepository {get;}   
 
    }
}