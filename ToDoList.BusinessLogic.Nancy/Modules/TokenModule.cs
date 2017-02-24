using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BusinessLogic.Nancy.Auth.JWT;
using ToDoList.BusinessLogic.Nancy.Auth.JWT.Interfaces;

namespace ToDoList.BusinessLogic.Nancy.Modules
{
    public class TokenModule : NancyModule
    {
        public TokenModule(ITokenEncoder encoder) : base("api/token")
        {
            Post["/"] = parameters =>
            {
                var user = this.Bind<User>();
                if(user.Login != null)
                {
                    var token = new JWTToken
                    {
                        UserName = user.Login,
                        Expire = DateTime.UtcNow.AddMinutes(30)
                    };
                    return encoder.Encode(token);
                }
                return HttpStatusCode.Unauthorized;
            };

        }
    }

    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
