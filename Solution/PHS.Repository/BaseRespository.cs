using PHS.Repository.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Repository.Repository.Core
{
    public abstract class BaseRespository<T, PrimaryKeyT>
         where T : class
    {
        public BaseRespository(PHSContext dc)
        {
            this.DataContext = dc;
        }

        public BaseRespository()
        {

        }

        public PHSContext DataContext;

        public abstract DbSet<T> EntitySet { get; }

        protected abstract T ConvertToNativeEntity(T entity);

        protected abstract PrimaryKeyT SelectPrimaryKey(T entity);

        public virtual PrimaryKeyT Save(T entity)
        {
            T native = ConvertToNativeEntity(entity);
            this.EntitySet.Add(native);
            this.DataContext.SaveChanges();
            return SelectPrimaryKey(native);
        }

        public virtual IQueryable<T> List()
        {
            return (from t in this.EntitySet
                    select t).Cast<T>();
        }

        protected virtual T GetByPrimaryKey(Func<T, bool> keySelection)
        {
            return (from r in this.EntitySet
                    select r).SingleOrDefault(keySelection) as T;
        }

        public virtual void Delete(PrimaryKeyT key)
        {
            this.EntitySet.Remove(GetByPrimaryKey(key) as T);
            this.DataContext.SaveChanges();
        }

        public virtual void SaveChanges()
        {
            this.DataContext.SaveChanges();
        }

        public virtual void Close()
        {
            this.DataContext.Dispose();
        }

        public abstract T GetByPrimaryKey(PrimaryKeyT key);

       
    }
}
