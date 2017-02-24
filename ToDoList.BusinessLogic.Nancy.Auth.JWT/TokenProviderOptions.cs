using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BusinessLogic.Nancy.Auth.JWT.Interfaces;

namespace ToDoList.BusinessLogic.Nancy.Auth.JWT
{
    public class TokenProviderOptions : ITokenProviderOptions
    {
        public string Path { get; set; }

        public string SecureKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["securekey"] ?? "SecureMySecurekey";
            }
            
        }
    }
}
