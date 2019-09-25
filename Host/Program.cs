using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Service;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(MyService)))
            {
                host.Open();
                Console.WriteLine("The host is listen at :");
                foreach (var item in host.Description.Endpoints)
                {
                    Console.WriteLine("{0} \n\t {1}", item.Address, item.Binding);
                }
                Console.WriteLine();
                Console.ReadLine();
            }
        }
    }
}
