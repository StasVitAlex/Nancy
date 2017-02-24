using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.BusinessLogic.Core.DTO
{
    public class ToDoDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
