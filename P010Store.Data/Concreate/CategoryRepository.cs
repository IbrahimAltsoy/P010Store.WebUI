using Microsoft.EntityFrameworkCore;
using P010Store.Data.Absract;
using P010Store.Data.Concrete;
using P010Store.Entities;

namespace P010Store.Data.Concreate
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext _context) : base(_context)
        {
        }

        public async Task<Category> GetCategoryByProducts(int id)
        {
            return await context.Categories.Where(c => c.Id == id).AsNoTracking().Include(p => p.Products).FirstOrDefaultAsync();
        }
    }
}
