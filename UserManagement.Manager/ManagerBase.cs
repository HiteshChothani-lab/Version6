using UserManagement.Common.Utilities;
using UserManagement.Manager.Mappers;

namespace UserManagement.Manager
{
    public class ManagerBase
    {
        protected readonly IConnectivity Connectivity;
        protected readonly IServiceEntityMapper Mapper;
        public ManagerBase(IConnectivity connectivity, IServiceEntityMapper mapper)
        {
            Connectivity = connectivity;
            Mapper = mapper;
        }
    }
}
