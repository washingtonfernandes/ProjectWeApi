using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DataContext context;

        public ProdutoRepository(DataContext context)
        {
            this.context = context;
        }

        
        public bool Delete(int idE)
        {
            var produto = context.Produtos.FirstOrDefault(i => i.Id == idE);
            if (produto == null)
                return false;
            else
            {
                context.Produtos.Remove(produto);
                return true;
            }
        }

        public async Task<List<Produto>> GetAllAsync()
        {
            return await context.Produtos.ToListAsync();
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await context.Produtos.SingleOrDefaultAsync(i => i.Id == id);
        }

        public void Save(Produto e) => context.Add(e);

        public void Update(Produto e)
        {
            context.Entry(e).State = EntityState.Modified;
        }
    }
}