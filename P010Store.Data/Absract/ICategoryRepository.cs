using P010Store.Data.Absract;
using P010Store.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P010Store.Data.Absract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetCategoryByProducts(int id);

    }
}
