﻿namespace UserManagement.Entity
{
    public class UpdateButtonsRequestEntity
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public long UserId { get; set; }
        public long SuperMasterId { get; set; }
        public string Button1 { get; set; }
        public string Button2 { get; set; }
        public string Button3 { get; set; }
        public string Button4 { get; set; }
    }
}
