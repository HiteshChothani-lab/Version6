﻿namespace UserManagement.Entity
{
    public class MoveStoreUserRequestEntity
    {
        public string MovedPosOid { get; set; }
        public string Mid { get; set; }
        public string OrderId { get; set; }
        public string MovedId { get; set; }
        public string NewCellNo { get; set; }
        public string OldCellNo { get; set; }
    }
}
