using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DataAccessLayer.Models;
using ToDoList.DataAccessLayer.Repositories.Interfaces;

namespace ToDoList.DataAccessLayer.Repositories.Repositories.DataRepositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>
    {
        public RefreshTokenRepository(IToDoDbContext dataContext) : base(dataContext)
        {
        }
    }
}

