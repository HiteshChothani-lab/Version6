using Prism.Events;
using UserManagement.UI.ItemModels;

namespace UserManagement.UI.Events
{
    public class MoveStoreUserSubmitEvent : PubSubEvent<MoveStoreUserItemModel>
    {

    }

    public class MoveStoreUserToArchiveSubmitEvent : PubSubEvent<MoveStoreUserItemModel>
    { }
}
