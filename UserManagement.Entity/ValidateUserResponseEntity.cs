namespace UserManagement.Entity
{
    public class ValidateUserResponseEntity : EntityBase
    {
        public string Username { get; set; }
        public string AccessCode { get; set; }
        public string AppVersionName { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Status { get; set; }
        public string Messagee { get; set; }
    }
}
