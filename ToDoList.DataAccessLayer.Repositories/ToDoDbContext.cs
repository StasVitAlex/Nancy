using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DataAccessLayer.Models;
using ToDoList.DataAccessLayer.Repositories.Interfaces;

namespace ToDoList.DataAccessLayer.Repositories
{
    public class ToDoDbContext : DbContext, IToDoDbContext
    {
        public ToDoDbContext() : base("name=ToDoEntities")
        {
        }

        public IDbSet<ToDo> ToDos { get; set; }
        public IDbSet<RefreshToken> RefreshTokens { get; set; }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        
        public void SaveChanges()
        {
            base.SaveChanges();
        }

    }
}
