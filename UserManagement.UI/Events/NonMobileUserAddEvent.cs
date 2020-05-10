using Prism.Events;
using UserManagement.UI.ItemModels;

namespace UserManagement.UI.Events
{
    public class NonMobileUserUpdateEvent : PubSubEvent<NonMobileUserItemModel>
    {
    }

    public class NonMobileUserEditEvent : PubSubEvent<NonMobileUserItemModel>
    {
    }
}
