using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BusinessLogic.Core.DTO;
using ToDoList.BusinessLogic.Core.IServices;
using ToDoList.DataAccessLayer.Models;
using ToDoList.DataAccessLayer.Repositories.Interfaces;
using ToDoList.DataAccessLayer.Repositories.Repositories.DataRepositories;

namespace ToDoList.BusinessLogic.Core.Services
{
    public class ToDoService : BaseApplicationService, IToDoService
    {
        public ToDoService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ToDoDto Create(ToDoDto item)
        {
            ToDoDto result = default(ToDoDto);
            try
            {
                this.InvokeInUnitOfWorkScope(uow =>
                {
                    var toDoRepo = uow.GetRepository<ToDoRepository>();
                    var toDo = new ToDo
                    {
                        Name = item.Name,
                        Active = item.Active
                    };
                    var addedItem = toDoRepo.Add(toDo);
                    uow.SaveChanges();
                    result = addedItem != null ? this.Mapper.Map<ToDo, ToDoDto>(addedItem) : null;
                });
            }
            catch (Exception ex)
            {
                return null;
            }
            return result;
        }


        public IEnumerable<ToDoDto> GetAll()
        {
            IEnumerable<ToDoDto> result = null;
            try
            {
                this.InvokeInUnitOfWorkScope(uow =>
                {
                   var toDoRepo =  uow.GetRepository<ToDoRepository>();
                    result = toDoRepo.Get().Select(res => this.Mapper.Map<ToDo, ToDoDto>(res));
                });
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public IEnumerable<ToDoDto> GetItems(bool completed)
        {
            IEnumerable<ToDoDto> result = null;
            try
            {
                this.InvokeInUnitOfWorkScope(uow =>
                {
                    var toDoRepo = uow.GetRepository<ToDoRepository>();
                    result = toDoRepo.Get(x => x.Active == completed).Select(res => new ToDoDto
                    {
                        ID = res.ID,
                        Name = res.Name,
                        Active = res.Active
                    });
                });
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public void GetItem()
        {
            throw new NotImplementedException();
        }

        public bool ChangeActiveStatus(int id, bool active)
        {
            bool result = false;
            try
            {
                this.InvokeInUnitOfWorkScope(uow =>
                {
                    var toDoRepo = uow.GetRepository<ToDoRepository>();
                    var toDo = toDoRepo.GetSingle(x => x.ID == id);
                    if(toDo == null)
                    {
                        result =  false;
                    }
                    else
                    {
                        toDo.Active = active;
                        toDoRepo.Modify(toDo);
                        uow.SaveChanges();
                        result = true;
                    }
                });
            }
            catch (Exception ex)
            {
                return false;
            }
            return result;
        }


        public bool Remove(int id)
        {
            bool result = false;
            try
            {
                this.InvokeInUnitOfWorkScope(uow =>
                {
                    var toDoRepo = uow.GetRepository<ToDoRepository>();
                    var toDo = toDoRepo.GetSingle(x => x.ID == id);
                    toDoRepo.Remove(toDo);
                    uow.SaveChanges();
                    result = true;
                });
            }
            catch (Exception ex)
            {
                return false;
            }
            return result;
        }
    }
}
