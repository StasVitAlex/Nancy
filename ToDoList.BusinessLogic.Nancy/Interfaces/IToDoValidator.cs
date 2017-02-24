using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BusinessLogic.Core.DTO;

namespace ToDoList.BusinessLogic.Nancy.Interfaces
{
    public interface IToDoValidator
    {
        ValidationResult Validate(ToDoDto item);
    }
}
