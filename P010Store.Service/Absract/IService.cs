using P010Store.Data.Absract;
using P010Store.Entities;


namespace P010Store.Service.Absract
{
    public interface IService<T> : IRepository<T> where T : class, IEntity, new()
    {
        
    }
}
