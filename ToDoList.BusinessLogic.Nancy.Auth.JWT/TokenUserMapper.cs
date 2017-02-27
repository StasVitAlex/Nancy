using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Security;
using ToDoList.BusinessLogic.Nancy.Auth.JWT.Interfaces;

namespace ToDoList.BusinessLogic.Nancy.Auth.JWT
{
    public class TokenUserMapper : ITokenUserMapper
    {
        public IUserIdentity Map(AccessToken token)
        {
            return new UserIdentity
            {
                Claims = token.Claims,
                UserName = token.UserName
            };
        }
    }
}
