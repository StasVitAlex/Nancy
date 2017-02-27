using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.BusinessLogic.Core.IServices
{
    public interface ITokenService
    {
        bool ValidateRefreshToken(string token);

        void UpdateRefreshToken(string oldToken, string newToken);

        void AddRefreshToken(string token);
    }
}
