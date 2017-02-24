using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DataAccessLayer.Repositories.Interfaces;

namespace ToDoList.DataAccessLayer.Repositories.Repositories
{
    public class BaseRepository<TEntity> : BaseSimpleRepository, IGenericRepository<TEntity> where TEntity : class
    {
       

        protected IDbSet<TEntity> EntitySet => this.Context.Set<TEntity>();

        public BaseRepository(IToDoDbContext context): base(context)
        {

        }

        public TEntity Add(TEntity entity)
        {
            return this.EntitySet.Add(entity);
        }

        public IEnumerable<TEntity> Get()
        {
            return this.EntitySet;
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> func)
        {
            return this.EntitySet.Where(func);
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> func)
        {
            return this.EntitySet
                .SingleOrDefault(func);
        }

        public void Remove(TEntity entity)
        {
            this.EntitySet.Remove(entity);
        }

        public void Modify(TEntity entity)
        {
            var entityEntry = this.Context.Entry(entity);

            if (entityEntry == null)
            {
                throw new ArgumentException("Entity not attached");
            }

            entityEntry.State = EntityState.Modified;
        }
    }
}
