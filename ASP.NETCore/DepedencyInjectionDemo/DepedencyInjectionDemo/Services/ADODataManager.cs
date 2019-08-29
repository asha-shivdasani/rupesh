using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepedencyInjectionDemo.Services
{
    public class ADODataManager : IDataManager
    {
        public string GetMessage(string name)
        {
            return $"Hello {name} from ADODataManager";
        }
    }
}
