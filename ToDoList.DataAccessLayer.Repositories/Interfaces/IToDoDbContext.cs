using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DataAccessLayer.Models;

namespace ToDoList.DataAccessLayer.Repositories.Interfaces
{
    public interface IToDoDbContext : IDisposable
    {
        IDbSet<ToDo> ToDos { get; set; }
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void SaveChanges();
    }
}
