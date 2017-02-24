using JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BusinessLogic.Nancy.Auth.JWT.Interfaces;

namespace ToDoList.BusinessLogic.Nancy.Auth.JWT
{
    public class JWTTokenEncoder : ITokenEncoder
    {
        #region Privates
        private string SecureKey;
        #endregion

        public JWTTokenEncoder(ITokenProviderOptions tokenOptions)
        {
            SecureKey = tokenOptions.SecureKey;
        }

        #region Implementations
        public string Encode(object payload)
        {
            return JsonWebToken.Encode(payload, SecureKey, JwtHashAlgorithm.HS256);
        }
        #endregion
    }
}
