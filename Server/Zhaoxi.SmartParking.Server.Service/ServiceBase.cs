using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Zhaoxi.SmartParking.Server.IService;

namespace Zhaoxi.SmartParking.Server.Service
{
    public class ServiceBase : IServiceBase
    {

        protected DbContext Context { get; private set; }

        public ServiceBase(IEFContext.IEFContext eFContext)
        {
            Context = eFContext.CreateDBContext();
        }

        public void Commit()
        {
            this.Context.SaveChanges();
        }

        public void Delete<T>(int Id) where T : class
        {
            var t = this.Find<T>(Id);

            if (t != null) throw new Exception("t is null");

            this.Context.Set<T>().Remove(t);

            this.Commit();
        }

        public void Delete<T>(T t) where T : class
        {
            if (t == null) throw new Exception("t is null");

            this.Context.Set<T>().Attach(t);

            this.Context.Set<T>().Remove(t);

            this.Commit();
        }

        public void Delete<T>(IEnumerable<T> tList) where T : class
        {
            foreach (var t in tList)
            {
                this.Context.Set<T>().Attach(t);
            }

            this.Context.Set<T>().RemoveRange(tList);

            this.Commit();
        }

        public T Find<T>(int id) where T : class
        {
            return this.Context.Set<T>().Find(id);
        }

        public T Insert<T>(T t) where T : class
        {
            this.Context.Set<T>().Add(t);

            this.Commit();

            return t;
        }

        public IEnumerable<T> Insert<T>(IEnumerable<T> tList) where T : class
        {
            this.Context.Set<T>().AddRange(tList);

            this.Commit();

            return tList;
        }

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class
        {
            return this.Context.Set<T>().Where(funcWhere);
        }

        public void Update<T>(T t) where T : class
        {
            if (t == null) throw new Exception("t is null");

            this.Context.Set<T>().Attach(t);

            this.Context.Entry(t).State = EntityState.Modified;

            this.Commit();

        }

        public void Update<T>(IEnumerable<T> tList) where T : class
        {
            foreach (var t in tList)
            {
                this.Context.Set<T>().Attach(t);

                this.Context.Entry(t).State = EntityState.Modified;
            }

            this.Commit();
        }

        public virtual void Dispose()
        {
            if (this.Context != null)
            {
                this.Context.Dispose();
            }
        }
    }
}
