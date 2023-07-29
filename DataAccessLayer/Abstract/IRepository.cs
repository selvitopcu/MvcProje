using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRepository<T> //Tden hangi tablo deperi gelirse
    {
        List<T> List();

        void Insert(T p); //T sınıfından gelen p nesnesi
        T Get(Expression<Func<T, bool>> filter);
        void Update(T p);
        void Delete(T p);
        List<T> List(Expression<Func<T, bool>> filter);

            //Burada eklediğimiz her şey diğer abstractlarda eklenecek
    }
}
