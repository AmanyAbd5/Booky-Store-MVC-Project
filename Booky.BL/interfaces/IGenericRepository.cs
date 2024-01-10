using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booky.BL.interfaces
{
    public interface IGenericRepository<T> where T : class
    {
       Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task add (T entity);
        void update (T entity);

        void delete (T entity);

    }
}
