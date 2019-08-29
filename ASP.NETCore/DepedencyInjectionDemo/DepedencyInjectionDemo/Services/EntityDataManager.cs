using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepedencyInjectionDemo.Services
{
    public class EntityDataManager : IDataManager
    {
        public string GetMessage(string name)
        {
            return $"Hello {name} from EntityDataManager";
        }
    }
}
