namespace UserManagement.WebServices.DataContracts.Request
{
    public class ValidateUserRequestContract
    {
        public string Username { get; set; }
        public string AccessCode { get; set; }
    }
}
