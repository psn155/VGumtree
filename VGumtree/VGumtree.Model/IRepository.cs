using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGumtree.Model
{
    public interface IRepository
    {
        int SaveChanges();
        IQueryable<T> GetQueryable<T>() where T : class;
        T GetById<T>(int id) where T : class, IIDModel;
        T Attach<T>(T entity) where T : class;
        T Add<T>(T entity) where T : class;
        T Delete<T>(T entity) where T : class;
        T DeleteById<T>(int id) where T : class, IIDModel;

        /// <summary>
        /// Disable change tracking and lazy loading by EF. 
        /// For Web API, we normally grab the data from the database and immediately return it to the client
        /// </summary>
        void DisableProxyCreation();
    }
}
