using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.BusinessLogic.Nancy
{
    public class NancySelfHost
    {
        private NancyHost m_nancyHost;

        public void Start()
        {
            m_nancyHost = new NancyHost(new Uri("http://localhost:52970/"));
            m_nancyHost.Start();

        }

        public void Stop()
        {
            m_nancyHost.Stop();
            Console.WriteLine("Stopped. Good bye!");
        }
    }
}
