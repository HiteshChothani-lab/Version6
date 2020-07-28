using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.WebServices.DataContracts.Request
{
    public class MoveStoreUserRequestContract
    {
        public string MovedPosOid { get; set; }
        public string Mid { get; set; }
        public string OrderId { get; set; }
        public string MovedId { get; set; }
        public string NewCellNo { get; set; }
        public string OldCellNo { get; set; }
    }
}
