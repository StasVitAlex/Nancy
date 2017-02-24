using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BusinessLogic.Core.DTO;
using ToDoList.BusinessLogic.Core.IServices;
using ToDoList.BusinessLogic.Nancy.Interfaces;
using ToDoList.BusinessLogic.Nancy.Validation;

namespace ToDoList.BusinessLogic.Nancy.Modules
{
    public class ToDoModule : NancyModule
    {

        public ToDoModule(IToDoService toDoService, IToDoValidator validator) : base("api/ToDos")
        {
            Get["/"] = _ =>
            {
                var toDoItems = toDoService.GetAll();
                return Response.AsJson(toDoItems.Select(x => new
                {
                    id = x.ID,
                    name = x.Name,
                    active = x.Active
                }),
                HttpStatusCode.OK);
            };

            Post["/"] = parameters =>
            {
                var toDo = this.Bind<ToDoDto>();
                var validationResult = validator.Validate(toDo);
                if (!validationResult.IsValid)
                {
                    return Negotiate.WithModel(validationResult).WithStatusCode(HttpStatusCode.BadRequest);
                }
                var result = toDoService.Create(toDo);
                if (result == null)
                {
                    return HttpStatusCode.InternalServerError;
                }
                return Response.AsJson(new
                {
                    id = result.ID,
                    name = result.Name,
                    active = result.Active
                });
            };

            Delete["/{id}"] = paramatres =>
            {
                var result = toDoService.Remove(paramatres.id);
                if (!result)
                {
                    return HttpStatusCode.InternalServerError;
                }
                return HttpStatusCode.OK;
            };

            Get["/completed"] = parameters =>
            {
                var items = toDoService.GetItems(active: true);
                return Response.AsJson(items);
            };

            Put["/{id}/completed"] = parameters =>
            {
                var toDo = this.Bind<ToDoDto>();
                var result = toDoService.ChangeActiveStatus(parameters.id, active: false);
                if (!result)
                {
                    return HttpStatusCode.InternalServerError;
                }
                return HttpStatusCode.OK;
            };

            Get["/active"] = parameters =>
            {
                var items = toDoService.GetItems(active: true);
                return Response.AsJson(items);
            };

            Put["/{id}/active"] = parameters =>
            {
                var toDo = this.Bind<ToDoDto>();
                var result = toDoService.ChangeActiveStatus(parameters.id, active: true);
                if (!result)
                {
                    return HttpStatusCode.InternalServerError;
                }
                return HttpStatusCode.OK;
            };

        }
    }
}
