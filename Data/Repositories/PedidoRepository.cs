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
    public class PedidoRepository : IPedidoRepository
    {
        private readonly DataContext context;
        public PedidoRepository(DataContext context)
        {
            this.context = context;
        }

        public bool Delete(int idE)
        {
            var pedido = context.Pedidos.FirstOrDefault(i => i.Id == idE);

            if (pedido == null)
                return false;
            else
            {
                context.Pedidos.Remove(pedido);
                return true;
            }
        }

        public async Task<List<Pedido>> GetAllAsync()
        {
            return await context.Pedidos.Include(x => x.Produtos).ToListAsync();
        }

        public async Task<Pedido> GetByIdAsync(int id)
        {
            return await context.Pedidos.Include(x => x.Produtos).SingleOrDefaultAsync(i => i.Id == id);
        }

        public void Save(Pedido e)
        {
            context.Pedidos.Add(e);        }

        public void Update(Pedido e)
        {
            context.Entry(e).State = EntityState.Modified;
        }
    }
}