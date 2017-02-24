using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.BusinessLogic.Nancy.Auth.JWT
{
    public class JWTToken
    {
        public string UserName { get; set; }

        public DateTime Expire { get; set; }

        public IEnumerable<string> Claims { get; set; }
        
    }
}
