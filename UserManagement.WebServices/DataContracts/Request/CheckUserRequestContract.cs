using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.WebServices.DataContracts.Request
{
    public class CheckUserRequestContract
    {
        public long MasterStoreId { get; set; }
        public long SuperMasterId { get; set; }
        public long CountryCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HomePhone { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Action { get; set; }
    }
}
