using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_API.Ropsitory
{
    public interface  IRopsitory<T,T2>
    {
        public int Insert(T entity);
        public  List<T> GetAll();
        public T GetById(T2 elment);
        public int Delete(T2 elment);
        public int Update(T2 elment,T entity);
    }
}
