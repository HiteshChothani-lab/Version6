using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Entity
{
    public class UpdateNonMobileStoreUserRequestEntity
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public long UserId { get; set; }
        public string HomePhone { get; set; }
        public string PostalCode { get; set; }
        public string MasterStoreId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public long SuperMasterId { get; set; }
    }
}
