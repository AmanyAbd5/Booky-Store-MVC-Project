using Booky.BL.interfaces;
using Booky.DAL.Data;
using Booky.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booky.BL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        // DI 
        private readonly BookyDbContext dbcontext;

        public GenericRepository(BookyDbContext context)
        {
            dbcontext = context;
        }

        public async Task add(T entity)
        {
          await  dbcontext.Set<T>().AddAsync(entity);
        }

        public void delete(T entity)
        {
            dbcontext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Product))
            {
                return  (IEnumerable<T>)dbcontext.Product.Include(c=>c.Category).ToList();   
                    
            }
            else
            {
                return await dbcontext.Set<T>().ToListAsync();
            }
            
        }

        public async Task<T> GetById(int id)
        {
            if (typeof(T) == typeof(Product))
            {
                return await dbcontext.Product.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id) as T;
            }
            else
            {
                return await dbcontext.Set<T>().FindAsync(id);
            }
                
               

        }

                

        public void update(T entity)
        {
            dbcontext.Set<T>().Update(entity);
        }
    }
}
