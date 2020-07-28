using Prism.Events;
using PusherClient;
using System.Threading.Tasks;
using UserManagement.Common.Constants;
using UserManagement.Common.Enums;
using UserManagement.Pushers.Events;
using UserManagement.Pushers.ItemModels;

namespace UserManagement.Pushers
{
    public static class Client
    {
        private static Pusher _pusher;
        private static Channel _eventChannel;
        private static IEventAggregator _eventAggregator;

        public static async Task Init(this IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _pusher = new Pusher(PusherData.AppKey, new PusherOptions
            {
                Authorizer = new HttpAuthorizer(PusherData.AuthorizerEndPoint),
                Cluster = PusherData.Cluster
            });

            _pusher.ConnectionStateChanged += _pusher_ConnectionStateChanged;
            _pusher.Error += _pusher_Error;

            // Setup private channel
            _eventChannel = _pusher.SubscribeAsync(PusherData.ChannelName).Result;
            _eventChannel.Subscribed += ChatChannel_Subscribed;

            // Inline binding!

            #region Store User Events

            _eventChannel.Bind(PusherData.AddStoreUser, (dynamic data) =>
            {
                PushData(PusherAction.Store, PusherData.AddStoreUser, data);
            });

            _eventChannel.Bind(PusherData.EditStoreUser, (dynamic data) =>
            {
                PushData(PusherAction.Store, PusherData.EditStoreUser, data);
            });

            _eventChannel.Bind(PusherData.DeleteStoreUser, (dynamic data) =>
            {
                PushData(PusherAction.Store, PusherData.DeleteStoreUser, data);
            });

            _eventChannel.Bind(PusherData.MoveStoreUser, (dynamic data) =>
            {
                PushData(PusherAction.Store, PusherData.MoveStoreUser, data);
            });

            _eventChannel.Bind(PusherData.FlagStoreUser, (dynamic data) =>
            {
                PushData(PusherAction.Store, PusherData.FlagStoreUser, data);
            });

            #endregion

            #region Archieve Store User Events

            _eventChannel.Bind(PusherData.AddArchieveStoreUser, (dynamic data) =>
            {
                PushData(PusherAction.Archieve, PusherData.AddArchieveStoreUser, data);
            });

            _eventChannel.Bind(PusherData.EditArchieveStoreUser, (dynamic data) =>
            {
                PushData(PusherAction.Archieve, PusherData.EditArchieveStoreUser, data);
            });

            _eventChannel.Bind(PusherData.DeleteArchieveUser, (dynamic data) =>
            {
                PushData(PusherAction.Archieve, PusherData.DeleteArchieveUser, data);
            });

            #endregion

            await _pusher.ConnectAsync();
        }

        private static void PushData(PusherAction action, string eventName, dynamic data)
        {
            RefreshDataPusherModel refreshDataPusherModel = new RefreshDataPusherModel()
            {
                Action = action,
                EventName = eventName,
                Data = data,
            };
            _eventAggregator.GetEvent<RefreshData>().Publish(refreshDataPusherModel);
        }

        private static void _pusher_Error(object sender, PusherException error)
        {
            System.Console.WriteLine("Pusher Error: " + error);
        }

        private static void _pusher_ConnectionStateChanged(object sender, ConnectionState state)
        {
            System.Console.WriteLine("Connection state: " + state);
        }

        private static void ChatChannel_Subscribed(object sender)
        {
            System.Console.WriteLine("ChatChannel_Subscribed");
        }

        public static void Disconnect()
        {
            var disconnectResult = Task.Run(() => _pusher.DisconnectAsync());
            Task.WaitAll(disconnectResult);
        }

        public static void Connect()
        {
            var connectResult = Task.Run(() => _pusher.ConnectAsync());
            Task.WaitAll(connectResult);
        }

        public static ConnectionState Status()
        {
            return _pusher.State;
        }

        public static async Task Dispose()
        {
            _eventChannel.UnbindAll();
            _eventChannel.Unsubscribe();
            _pusher.UnbindAll();

            _eventChannel.Subscribed -= ChatChannel_Subscribed;
            _pusher.ConnectionStateChanged -= _pusher_ConnectionStateChanged;
            _pusher.Error -= _pusher_Error;

            await _pusher.DisconnectAsync();

            _eventChannel = null;
            _pusher = null;
        }
    }
}
