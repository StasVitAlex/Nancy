using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DataAccessLayer.Repositories.Interfaces
{
    interface IGenericRepository<TEntity> where TEntity:class
    {
        TEntity Add(TEntity entity);

        void Modify(TEntity entity);

        TEntity GetSingle(Expression<Func<TEntity, bool>> func);

        IEnumerable<TEntity> Get();

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> func);

        void Remove(TEntity entity);
    }
}
