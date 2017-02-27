using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BusinessLogic.Nancy;
using Topshelf;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
           NancyService.Run();
        }
    }
}
