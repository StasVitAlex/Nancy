using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BusinessLogic.Core.DTO;

namespace ToDoList.BusinessLogic.Core.IServices
{
    public interface IToDoService
    {
        void GetItem();
        ToDoDto Create(ToDoDto item);
        bool ChangeActiveStatus(int id, bool active);
        IEnumerable<ToDoDto> GetItems(bool active);
        bool Remove(int id);
        IEnumerable<ToDoDto> GetAll();
    }
}
