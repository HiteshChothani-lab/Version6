namespace UserManagement.Entity
{
    public class EntityBase
    {
        public bool IsSuccess { get; set; } = false;
        public int StatusCode { get; set; }
        public string Message { get; set; } = "";
    }
}
