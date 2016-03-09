using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Linq.Expressions;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using NEC_NS_Evidencija.Backend.DatabaseLayer;

namespace NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common
{
    public abstract class RepositoryBase<T> where T : class
    {
        private MyEntities dataContext;
        protected readonly IDbSet<T> dbset;
        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = GetDataContext().Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        public MyEntities GetDataContext()
        {
            if (dataContext == null)
            {
                dataContext = DatabaseFactory.Get();
                dataContext.Database.Log = Log;
            }
            return dataContext ?? (dataContext = DatabaseFactory.Get());
        }
        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }
        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }
        public virtual T GetById(long id)
        {
            return dbset.Find(id);
        }
        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }
        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }


        //TODO: Support for multiple includeExpressions
        public T Get(Expression<Func<T, bool>> where, string include)
        {
            return dbset.Where(where).Include(include).FirstOrDefault<T>();
        }

        public int Commit()
        {
            return dataContext.SaveChanges();
        }

        public void Detach(T entity)
        {
            GetDataContext().Entry(entity).State = EntityState.Unchanged;
        }

        protected void Log(string line)
        {
        }
    }
}
