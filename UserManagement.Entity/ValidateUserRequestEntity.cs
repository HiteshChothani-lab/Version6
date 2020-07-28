using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Entity
{
    public class ValidateUserRequestEntity
    {
        public string Username { get; set; }
        public string AccessCode { get; set; }
    }
}
