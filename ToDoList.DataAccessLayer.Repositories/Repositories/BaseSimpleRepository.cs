using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DataAccessLayer.Repositories.Interfaces;

namespace ToDoList.DataAccessLayer.Repositories.Repositories
{
    public class BaseSimpleRepository
    {
        public BaseSimpleRepository(IToDoDbContext dataContext)
        {
            this.Context = dataContext;
            if (this.Context == null)
            {
                throw new ArgumentException("Invalid context");
            }
        }

        protected IToDoDbContext Context
        {
            get;
            private set;
        }
    }
}
