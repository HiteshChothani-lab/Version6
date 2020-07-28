using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.WebServices.DataContracts.Request
{
    public class DeleteArchiveUserRequestContract 
    {
        public string MasterStoreId { get; set; }
        public string UserId { get; set; }
        public string Id { get; set; }
        public long SuperMasterId { get; set; }
    }
}
