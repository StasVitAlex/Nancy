using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BusinessLogic.Nancy.Auth.JWT.Interfaces;

namespace ToDoList.BusinessLogic.Nancy.Auth.JWT
{
    public class TokenConfiguration : ITokenConfiguration
    {
        public ITokenProviderOptions Configure()
        {
            return new TokenProviderOptions
            {
                Path = "/api/token"
            };
        }
    }
}
