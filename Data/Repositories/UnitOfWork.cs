using Data.Context;
using Domain.Interfaces;

namespace Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            this._context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        private IClienteRepository _clienteRepository;
        public IClienteRepository ClienteRepository
        {
            get{ return _clienteRepository ??= new ClienteRepository(_context);}
        }

        private IPedidoRepository _pedidoRepository;
        public IPedidoRepository PedidoRepository
        {
            get{ return _pedidoRepository ??= new PedidoRepository(_context);}
        }

        private IVendedorRepository _vendedorRepository;
        public IVendedorRepository VendedorRepository
        {
            get{ return _vendedorRepository ??= new VendedorRepository(_context);}
        }

        private IProdutoRepository _produtoRepository;
        public IProdutoRepository ProdutoRepository
        {
            get{ return _produtoRepository ??= new ProdutoRepository(_context);}
        }

        private IPagamentoRepository _pagamentoRepository;
        public IPagamentoRepository PagamentoRepository
        {
            get { return _pagamentoRepository ??= new PagamentoRepository(_context);}
        }

        private ICategoriaRepository _categoriaRepository;
        public ICategoriaRepository CategoriaRepository
        {
            get{ return _categoriaRepository ??= new CategoriaRepository(_context);}
        }
    }
}