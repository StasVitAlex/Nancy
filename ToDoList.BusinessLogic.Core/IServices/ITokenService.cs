using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.BusinessLogic.Core.IServices
{
    public interface ITokenService
    {
        bool Validate(string token);

        void UpdateRefreshToken(string token);

        void AddRefreshToken(string token);
    }
}
