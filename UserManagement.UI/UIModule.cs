using DryIoc;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using UserManagement.Common.Constants;
using UserManagement.Manager;
using UserManagement.UI.ViewModels;
using UserManagement.UI.Views;

namespace UserManagement.UI
{
    public class UIModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IContainer _container;
        private readonly IWindowsManager _windowsManager;
        private readonly IEventAggregator _eventAggregator;

        public UIModule(IContainer container, IRegionManager regionManager,
            IWindowsManager windowsManager, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _container = container;
            _regionManager = regionManager;
            _windowsManager = windowsManager;
        }
        

        public async void OnInitialized(IContainerProvider containerProvider)
        {
            if (Config.MasterStore != null)
            {
                PusherData.MasterStoreID = Config.MasterStore.StoreId.ToString();
                await Pushers.Client.Init(_eventAggregator);

                var parameters = new NavigationParameters
                {
                    { NavigationConstants.RegisteredMasterStore, Config.MasterStore }
                };
                _regionManager.RequestNavigate("ContentRegion", ViewNames.MainPage, parameters);
            }
            else
            _regionManager.RequestNavigate("ContentRegion", ViewNames.StoreValidationPage);
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterMasterStore1Page, RegisterMasterStore1PageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterMasterStore2Page, RegisterMasterStore2PageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterMasterStoreReviewPage, RegisterMasterStoreReviewPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterMasterStoreFinishPage, RegisterMasterStoreFinishPageViewModel>();
            containerRegistry.RegisterForNavigation<StoreValidationPage, StoreValidationPageViewModel>();

            containerRegistry.RegisterForNavigation<NonMobileUserPopupPage>();
            containerRegistry.RegisterForNavigation<EditUserPopupPage>();
            containerRegistry.RegisterForNavigation<EditAppointmentPopupPage>();
            containerRegistry.RegisterForNavigation<UpdateNonMobileUserPopupPage>();
            containerRegistry.RegisterForNavigation<RegisterUserPopupPage>();
            containerRegistry.RegisterForNavigation<MoveUserPopupPage>();
            containerRegistry.RegisterForNavigation<SetRoomNumberPopUpPage>();
            containerRegistry.RegisterForNavigation<ExpressTimePickerPopupPage>();
        }
    }
}
