using UserManagement.Common.Enums;

namespace UserManagement.Pushers.ItemModels
{
    public class RefreshDataPusherModel
    {
        public string EventName { get; set; }
        public PusherAction Action { get; set; }
        public object Data { get; set; }
    }
}
