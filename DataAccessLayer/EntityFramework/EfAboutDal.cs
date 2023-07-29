using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfAboutDal : GenericRepository<About>, IAboutDal
    {
        Context c = new Context();
        public void ChangeToFalse(int id)
        {
            var values = c.Abouts.Find(id);
            values.Status = false;
     
            c.SaveChanges();
        }

        public void ChangeToTrue(int id)
        {
            var values = c.Abouts.Find(id);
            values.Status = true;

            c.SaveChanges();
        }
    }
}
