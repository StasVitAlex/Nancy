using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BusinessLogic.Core.IServices;
using ToDoList.DataAccessLayer.Models;
using ToDoList.DataAccessLayer.Repositories.Interfaces;
using ToDoList.DataAccessLayer.Repositories.Repositories.DataRepositories;

namespace ToDoList.BusinessLogic.Core.Services
{
    public class TokenService : BaseApplicationService, ITokenService
    {
        public TokenService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void UpdateRefreshToken(string token)
        {
            throw new NotImplementedException();
        }

        public bool Validate(string token)
        {
            return this.InvokeInUnitOfWorkScope(uow =>
                 {
                     var tokenRepo = uow.GetRepository<RefreshTokenRepository>();
                     var dbToken = tokenRepo.GetSingle(x => x.Token.Equals(token));
                     if (dbToken == null)
                     {
                         return false;
                     }
                     if(!dbToken.IsActive || dbToken.ExpirationDate < DateTime.UtcNow)
                     {
                         return false;
                     }
                     return true;
                 });
        }

        public void AddRefreshToken(string token)
        {
            this.InvokeInUnitOfWorkScope(uow =>
            {
                var tokenRepo = uow.GetRepository<RefreshTokenRepository>();
                var refreshToken = new RefreshToken
                {
                    ExpirationDate = DateTime.UtcNow.AddDays(7),
                    IsActive = true,
                    Token  = token
                };
                tokenRepo.Add(refreshToken);
                uow.SaveChanges();
            });
        }

    }
}
}
