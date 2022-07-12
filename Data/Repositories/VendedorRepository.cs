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
    public class VendedorRepository : IVendedorRepository
    {
        private readonly DataContext context;

        public VendedorRepository(DataContext context)
        {
            this.context = context;
        }

        
        public bool Delete(int idE)
        {
            var vendedor = context.Vendedores.FirstOrDefault(i => i.Id == idE);

            if (vendedor == null)
                return false;
            else
            {
                context.Vendedores.Remove(vendedor);
                return true;
            }
        }

        public async Task<List<Vendedor>> GetAllAsync()
        {
            return await context.Vendedores.ToListAsync();
        }

        public async Task<Vendedor> GetByIdAsync(int id)
        {
            return await context.Vendedores.SingleOrDefaultAsync(i => i.Id == id);
        }

        public void Save(Vendedor e)
        {
            context.Add(e);
        }

        public void Update(Vendedor e)
        {
            context.Entry(e).State = EntityState.Modified;
        }
    }
}