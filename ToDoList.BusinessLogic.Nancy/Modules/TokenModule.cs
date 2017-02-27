using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BusinessLogic.Core.IServices;
using ToDoList.BusinessLogic.Nancy.Auth.JWT;
using ToDoList.BusinessLogic.Nancy.Auth.JWT.Interfaces;

namespace ToDoList.BusinessLogic.Nancy.Modules
{
    public class TokenModule : NancyModule
    {
        public TokenModule(ITokenEncoder encoder, ITokenService tokenService) : base("api/token")
        {
            Post["/"] = parameters =>
            {
                var user = this.Bind<User>();
                if (user.Login == null)
                {
                    return HttpStatusCode.Unauthorized;
                }

                switch (user.GrantType.ToLower())
                {
                    case "client_credentials":
                        return GenerateTokenByCredentials(user, tokenService);
                        break;
                    case "refresh_token":
                       return GenerateTokenByCredentials(user, tokenService);
                        break;
                    default:
                        return HttpStatusCode.BadRequest;
                        break;
                }
            };

        }

        private Response GenerateTokenByCredentials(User user, ITokenService tokenService)
        {
            var refreshToken = new RefreshToken
            {
                id = Guid.NewGuid(),
                ExpirationDate = DateTime.UtcNow.AddDays(7),
            };
            var accessToken = new AccessToken
            {
                UserName = user.Login,
                Expire = DateTime.UtcNow.AddMinutes(3)
            };


            return Response.AsJson(new
            {
                RefreshToken = refreshToken,
                AccessToken = accessToken
            });
        }

    }

    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string GrantType { get; set; }

    }
}
