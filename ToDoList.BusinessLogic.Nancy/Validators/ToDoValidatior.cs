using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BusinessLogic.Core.DTO;
using ToDoList.BusinessLogic.Nancy.Interfaces;
using FluentValidation.Results;

namespace ToDoList.BusinessLogic.Nancy.Validation
{
    public class ToDoValidator : AbstractValidator<ToDoDto>, IToDoValidator
    {
        public ToDoValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage("You must specify a name.");
        }

        public override ValidationResult Validate(ToDoDto instance)
        {
            return base.Validate(instance);
        }
    }
}
