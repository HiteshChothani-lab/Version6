using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Entity
{
    public class DeleteStoreUserRequestEntity
    {
        public string MasterStoreId { get; set; }
        public string Id { get; set; }
        public string UserId { get; set; }
        public string OrphanStatus { get; set; }
        public long SuperMasterId { get; set; }
    }
}
