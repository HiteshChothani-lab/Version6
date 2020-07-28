using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Common.Utilities
{
    public interface IConnectivity
    {
        bool IsInternetAvailable { get; }
    }
}
