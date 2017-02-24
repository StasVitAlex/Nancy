using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DataAccessLayer.Repositories.Interfaces;
using ToDoList.DataAccessLayer.Repositories.Repositories;

namespace ToDoList.DataAccessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IToDoDbContext Context { get; set; }

        public UnitOfWork(IToDoDbContext context)
        {
            this.Context = context;
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        public TRepo GetRepository<TRepo>() where TRepo : BaseSimpleRepository
        {
            return Activator.CreateInstance(typeof(TRepo), this.Context) as TRepo;
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }
    }
}
