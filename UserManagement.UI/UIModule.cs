using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using UserManagement.Common.Constants;
using UserManagement.UI.ViewModels;
using UserManagement.UI.Views;

namespace UserManagement.UI
{
    public class UIModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        public UIModule(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
        }

		public async void OnInitialized(IContainerProvider containerProvider)
		{
            if (Config.MasterStore != null)
            {
                PusherData.MasterStoreID = Config.MasterStore.StoreId.ToString();
                await Pushers.Client.Init(_eventAggregator);

                var parameters = new NavigationParameters();
                parameters.Add(NavigationConstants.RegisteredMasterStore, Config.MasterStore);
                _regionManager.RequestNavigate("ContentRegion", ViewNames.MainPage, parameters);
            }
            else
            {
                _regionManager.RequestNavigate("ContentRegion", ViewNames.StoreValidationPage);
            }
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
            containerRegistry.RegisterForNavigation<EditButtonsPopupPage>();
            containerRegistry.RegisterForNavigation<UpdateNonMobileUserPopupPage>();
            containerRegistry.RegisterForNavigation<MoveUserPopupPage>();
            containerRegistry.RegisterForNavigation<RegisterUserPopupPage>();
        }
    }
}
