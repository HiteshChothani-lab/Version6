using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
