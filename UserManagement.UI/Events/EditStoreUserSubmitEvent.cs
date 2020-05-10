using Prism.Events;
using UserManagement.UI.ItemModels;

namespace UserManagement.UI.Events
{
    public class EditStoreUserSubmitEvent :  PubSubEvent<EditStoreUserItemModel>
    {
    }
    public class SetRoomNumberSubmitEvent : PubSubEvent<string>
        
    {
    }
    
}
