using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Entity
{
    public class DefaultResponseEntity : EntityBase
    {
        public string Status { get; set; }
        public string Messagee { get; set; }
    }
}
