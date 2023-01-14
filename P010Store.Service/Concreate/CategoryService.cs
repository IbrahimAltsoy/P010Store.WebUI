using P010Store.Data;
using P010Store.Data.Absract;
using P010Store.Data.Concreate;
using P010Store.Service.Absract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P010Store.Service.Concreate
{
    public class CategoryService : CategoryRepository, ICategoryService
    {
        public CategoryService(DatabaseContext _context) : base(_context)
        {
        }
    }
}
