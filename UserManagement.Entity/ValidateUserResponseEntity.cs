using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Entity
{
    public class ValidateUserResponseEntity : EntityBase
    {
        public string AppVersionName { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public string Messagee { get; set; }
        public string Username { get; set; }
        public string AccessCode { get; set; }
        public string Token { get; set; }
    }
}
