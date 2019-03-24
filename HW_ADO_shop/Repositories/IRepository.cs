using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_ADO_shop.Repositories
{
    public interface IRepository<T>
    {
        void Add(T obj);
        T Read(int id);        
        int Delete(int id);
        int Update(int id, T updated);
    }
}
