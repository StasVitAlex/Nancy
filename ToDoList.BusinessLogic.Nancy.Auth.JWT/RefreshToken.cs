using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.BusinessLogic.Nancy.Auth.JWT
{
    public class RefreshToken
    {
        public Guid id { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
