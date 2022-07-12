using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
       public interface IBaseRepository<E>
    where E:class
    {
        Task<E> GetByIdAsync(int id);
        Task<List<E>> GetAllAsync();
        void Save(E e);
        bool Delete(int idE);
        void Update(E e);
    }
}