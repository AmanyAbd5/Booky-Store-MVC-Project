using Booky.BL.interfaces;
using Booky.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booky.BL.Repository
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly BookyDbContext dbContext;
        public ICategoryRepository CategoryRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }

        public UnitOfWork(BookyDbContext dbContext) 
        {
            CategoryRepository= new CategoryRepository(dbContext);
            ProductRepository = new ProductRepository(dbContext);
            this.dbContext= dbContext;
            
        }

        public async Task<int> save()
        => await dbContext.SaveChangesAsync();
         
        public void Dispose()
        =>dbContext.Dispose();
    }
}
