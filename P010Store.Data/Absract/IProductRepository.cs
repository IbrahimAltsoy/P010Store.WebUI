using P010Store.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P010Store.Data.Absract
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsByCategoriesBrandsAsync();
        Task<Product> GetProductByCategoriesBrandsAsync(int id);

    }
}
