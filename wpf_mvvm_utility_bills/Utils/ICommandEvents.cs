using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_mvvm_utility_bills.Utils
{
    public interface ICommandEvents
    {
        public event Action OnSuccess;
        public event Action<Exception> OnError;
    }
}
