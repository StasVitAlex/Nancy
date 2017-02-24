using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DataAccessLayer.Models
{
    public class ToDo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
