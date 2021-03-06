﻿using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.BusinessLogic.Nancy.Auth.JWT.Interfaces
{
    public interface ITokenUserMapper
    {
        IUserIdentity Map(JWTToken token);
    }
}
