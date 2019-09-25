using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Discovery;
using System.ServiceModel;
using Contract;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //DefaultWCFDiscovery();

            DynamicEndpointWCFDiscovery();
        }

        private static void DefaultWCFDiscovery()
        {
            var client = new DiscoveryClient(new UdpDiscoveryEndpoint());
            var criteria = new FindCriteria(typeof(IMyService));
            var findResult = client.Find(criteria);

            foreach (var item in findResult.Endpoints)
            {
                Console.WriteLine(item.Address);
            }
            Console.WriteLine();
            var address = findResult.Endpoints.First(ep => ep.Address.Uri.Scheme == "http").Address;
            Console.WriteLine(address);

            var factory = new ChannelFactory<IMyService>(new BasicHttpBinding(), address);
            var proxy = factory.CreateChannel();
            Console.WriteLine(proxy.GetData(42));

            ((ICommunicationObject)proxy).Close();
            Console.ReadLine();
        }

        private static void DynamicEndpointWCFDiscovery()
        {
            foreach (var binding in new Binding[] { new BasicHttpBinding(),
            new WSHttpBinding(), new NetTcpBinding()})
            {
                var endpoint = new DynamicEndpoint(ContractDescription.GetContract(typeof(IMyService)), binding);
                var factory = new ChannelFactory<IMyService>(endpoint);
                var proxy = factory.CreateChannel();
                Console.WriteLine(proxy.GetData(Environment.TickCount));
                ((ICommunicationObject)proxy).Close();
            }
            Console.ReadLine();
        }
    }

}
