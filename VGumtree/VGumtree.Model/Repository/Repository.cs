using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace VGumtree.Model
{
    public class Repository : IRepository, IDisposable
    {
        private VGumtreeDb _db;

        public Repository(VGumtreeDb db)
        {
            _db = db;
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public IQueryable<T> GetQueryable<T>() where T : class
        {
            return _db.Set<T>();
        }

        public T GetById<T>(int id) where T : class, IIDModel
        {
            return _db.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public T Attach<T>(T entity) where T : class
        {
            var entry = _db.Entry<T>(entity);
            entry.State = System.Data.EntityState.Modified;
            return entity;
        }

        public T Add<T>(T entity) where T : class
        {
            return _db.Set<T>().Add(entity);
        }

        public T Delete<T>(T entity) where T : class
        {
            return _db.Set<T>().Remove(entity);
        }

        public T DeleteById<T>(int id) where T : class, IIDModel
        {
            var entity = _db.Set<T>().FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                return _db.Set<T>().Remove(entity);
            }
            return null;
        }

        public void DisableProxyCreation()
        {
            _db.Configuration.ProxyCreationEnabled = false;
        }


        public void Dispose()
        {
            if (_db != null)
                _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
