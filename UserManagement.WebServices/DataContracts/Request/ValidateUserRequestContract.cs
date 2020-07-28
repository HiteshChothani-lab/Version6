using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.WebServices.DataContracts.Request
{
    public class ValidateUserRequestContract
    {
        public string Username { get; set; }
        public string AccessCode { get; set; }
    }
}
