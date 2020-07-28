using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Entity
{
    public class ManageUserRequestEntity
    {
        public string Id { get; set; }
        public string MasterStoreId { get; set; }
        public long SuperMasterId { get; set; }
    }
}
