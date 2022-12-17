using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P010Store.Entities
{
    public interface IEntity // veritabanı classlarımızı işaretlemek için kullandık.
    {
        int Id { get; set; }
    }
}
