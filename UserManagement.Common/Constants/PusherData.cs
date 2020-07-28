namespace UserManagement.Common.Constants
{
    public static class PusherData
    {
        public static string MasterStoreID { get; set; }

        public static string AppKey { get; } = "23dfe3bfb4517feee985";
        public static string AuthorizerEndPoint { get; } = "https://tuco-app.herokuapp.com/api/windows/";
        public static string Cluster { get; } = "us2";
        public static string ChannelName { get; } = "tuco-app";

        public static string AddStoreUser { get { return $"event-add-{MasterStoreID}"; } }
        public static string EditStoreUser { get { return $"event-edit-{MasterStoreID}"; } }
        public static string DeleteStoreUser { get { return $"event-delete-{MasterStoreID}"; } }
        public static string MoveStoreUser { get { return $"event-edit-move-{MasterStoreID}"; } }
        public static string FlagStoreUser { get { return $"event-flag-{MasterStoreID}"; } }

        public static string AddArchieveStoreUser { get { return $"event-add-archive-{MasterStoreID}"; } }
        public static string EditArchieveStoreUser { get { return $"event-edit-archive-{MasterStoreID}"; } }
        public static string DeleteArchieveUser { get { return $"event-delete-archive-{MasterStoreID}"; } }
    }
}
