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
        #region Privates
        private ITokenService TokenService;

        private ITokenEncoder Encoder;
        #endregion

        public TokenModule(ITokenEncoder encoder, ITokenService tokenService) : base("api/token")
        {
            TokenService = tokenService;
            Encoder = encoder;

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
                        return GenerateTokenByCredentials(user);
                        break;
                    case "refresh_token":
                       return GenerateTokenByRefreshToken(user);
                        break;
                    default:
                        return HttpStatusCode.BadRequest;
                        break;
                }
            };

        }

        private Response GenerateTokenByCredentials(User user)
        {
            var refreshTokenObj = new RefreshToken
            {
                id = Guid.NewGuid(),
                ExpirationDate = DateTime.UtcNow.AddDays(7),
            };
            var accessTokenObj = new AccessToken
            {
                UserName = user.Login,
                Expire = DateTime.UtcNow.AddMinutes(3)
            };

            var accessToken = Encoder.Encode(accessTokenObj);
            var refreshToken = Encoder.Encode(refreshTokenObj);
            TokenService.AddRefreshToken(refreshToken);

            return Response.AsJson(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken

            });
        }

        private Response GenerateTokenByRefreshToken(User user)
        {
            var isValid = TokenService.ValidateRefreshToken(user.RefreshToken);
            if(isValid)
            {
                var refreshTokenObj = new RefreshToken
                {
                    id = Guid.NewGuid(),
                    ExpirationDate = DateTime.UtcNow.AddDays(7),
                };
                var accessTokenObj = new AccessToken
                {
                    UserName = user.Login,
                    Expire = DateTime.UtcNow.AddMinutes(3)
                };

                var accessToken = Encoder.Encode(accessTokenObj);
                var newRefreshToken = Encoder.Encode(refreshTokenObj);
                TokenService.UpdateRefreshToken(user.RefreshToken, newRefreshToken);

                return Response.AsJson(new
                {
                    AccessToken = accessToken,
                    RefreshToken = newRefreshToken
                });
            }
            return HttpStatusCode.BadRequest;
        }

    }

    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string GrantType { get; set; }
        public string RefreshToken { get; set; }

    }
}
