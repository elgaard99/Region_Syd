using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Region_Syd.Model
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        void Update(T entity);
    }
}
