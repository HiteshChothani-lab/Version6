namespace UserManagement.Common.Utilities
{
    public class Connectivity : IConnectivity
    {
        public bool IsInternetAvailable => InternetAvailability.IsInternetAvailable();
    }
}
