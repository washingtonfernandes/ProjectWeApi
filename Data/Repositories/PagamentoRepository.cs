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
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly DataContext context;
        public PagamentoRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<Pagamento> GetByIdAsync(int id)
        {
            return await context.Pagamentos.SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Pagamento>> GetAllAsync()
        {
            return await context.Pagamentos.ToListAsync();
        }

        public void Save(Pagamento e)
        {
            context.Pagamentos.Add(e);
        }

        public bool Delete(int idE)
        {
            var pagamento = context.Pagamentos.FirstOrDefault(i => i.Id == idE);

            if (pagamento == null)
                return false;
            else
            {
                context.Pagamentos.Remove(pagamento);
                return true;
            }
        }

        public void Update(Pagamento e)
        {
            context.Entry(e).State = EntityState.Modified;
        }
    }
}