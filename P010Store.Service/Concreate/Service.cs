using P010Store.Data;
using P010Store.Data.Concrete;
using P010Store.Entities;
using P010Store.Service.Absract;

namespace P010Store.Service.Concreate
{
    public class Service<T> : Repository<T>, IService<T> where T : class, IEntity, new()
    {
        public Service(DatabaseContext _context) : base(_context)
        {


        }         

       
    }
}
