using Microsoft.EntityFrameworkCore;
using P010Store.Data.Absract;
using P010Store.Data.Concrete;
using P010Store.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P010Store.Data.Concreate
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<Product>> GetAllProductsByCategoriesBrandsAsync()
        {
           return await context.Products.Include(c=>c.Category).Include(b=>b.Brand).ToListAsync();
        }
        public async Task<Product> GetProductByCategoriesBrandsAsync(int id)
        {
            return await context.Products.Include(c => c.Category).Include(b => b.Brand).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
