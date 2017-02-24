using JWT;
using Nancy.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BusinessLogic.Nancy.Auth.JWT.Interfaces;

namespace ToDoList.BusinessLogic.Nancy.Auth.JWT
{
    public class TokenValidator : ITokenValidator
    {

        private readonly ITokenProviderOptions tokenOptions;
        private readonly ITokenUserMapper mapper;

        public TokenValidator(ITokenProviderOptions tokenProvider, ITokenUserMapper userMapper)
        {
            this.tokenOptions = tokenProvider;
            this.mapper = userMapper;
        }


        #region ITokenValidator implementation

        public IUserIdentity ValidateUser(string token)
        {
            try
            {
                string securekey = this.tokenOptions.SecureKey;

#if DEBUG
                Console.WriteLine(securekey);
#endif

                var decoded = JsonWebToken.DecodeToObject(token, securekey) as Dictionary<string, object>;

                JWTToken tk = new JWTToken
                {
                    UserName = decoded["UserName"].ToString(),
                    Expire = DateTime.Parse(decoded["Expire"].ToString())
                };

                if (decoded.ContainsKey("Claims"))
                {
                    var claims = new List<string>();
                    var decodedClaims = (ArrayList)decoded["Claims"];
                    if (decodedClaims != null)
                    {


                        for (int i = 0; i < decodedClaims.Count; i++)
                        {
                            string claim = decodedClaims[i].ToString();
                            claims.Add(claim);
                        }

                        tk.Claims = claims;
                    }
                }

                if (tk.Expire < DateTime.UtcNow)  // expires
                    return null;

                return this.mapper.Map(tk);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
