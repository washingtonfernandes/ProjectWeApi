using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DataContext context;

        public ClienteRepository(DataContext context)
        {
            this.context = context;
        }

        

        public bool Delete(int idE)
        {
            var cliente = context.Clientes.FirstOrDefault(i => i.Id == idE);

            if (cliente == null)
                return false;
            else
            {
                context.Clientes.Remove(cliente);
                return true;
            }
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            return await context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            return await context.Clientes.SingleOrDefaultAsync(i => i.Id == id);
        }

        public void Save(Cliente e)
        {
            context.Clientes.Add(e);
        }

        public void Update(Cliente e)
        {
            context.Entry(e).State = EntityState.Modified;
        }
    }
}