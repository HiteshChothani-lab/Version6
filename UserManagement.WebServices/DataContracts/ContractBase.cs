using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.WebServices.DataContracts
{
    public class ContractBase
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } = "";
        public int StatusCode { get; set; } = 99;
    }
}
