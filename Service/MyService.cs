using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Contract;

namespace Service
{
    public class MyService : IMyService
    {
        public string GetData(int value)
        {
            var result = string.Format("You enterd \"{0}\" via \"{1}\"",
                value,
                OperationContext.Current.RequestContext.RequestMessage.Headers.To);
            Console.WriteLine(result);
            return result;
        }
    }
}
