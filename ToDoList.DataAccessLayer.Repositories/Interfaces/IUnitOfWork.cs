using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DataAccessLayer.Repositories.Repositories;

namespace ToDoList.DataAccessLayer.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        TRepo GetRepository<TRepo>() where TRepo : BaseSimpleRepository;
        void SaveChanges();
    }
}
