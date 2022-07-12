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
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DataContext context;

        public CategoriaRepository(DataContext context)
        {
            this.context = context;
        }

        
        public bool Delete(int idE)
        {
            var categoria = context.Categorias.FirstOrDefault(i => i.Id == idE);

            if (categoria == null)
                return false;
            else
            {
                context.Categorias.Remove(categoria);
                return true;
            }
        }

        public async Task<List<Categoria>> GetAllAsync()
        {
            return await context.Categorias.ToListAsync();
        }

        public async Task<Categoria> GetByIdAsync(int id)
        {
            return await context.Categorias.SingleOrDefaultAsync(i => i.Id == id);
        }

        public void Save(Categoria e)
        {
            context.Add(e);
        }

        public void Update(Categoria e)
        {
            context.Entry(e).State = EntityState.Modified;
        }
    }
}