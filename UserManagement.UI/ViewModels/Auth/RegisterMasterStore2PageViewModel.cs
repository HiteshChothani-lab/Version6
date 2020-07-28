using Prism.Commands;
using Prism.Regions;
using System;
using System.Linq;
using UserManagement.Common.Constants;
using UserManagement.UI.ItemModels;

namespace UserManagement.UI.ViewModels
{
	public class RegisterMasterStore2PageViewModel : ViewModelBase
	{
		public RegisterMasterStore2PageViewModel(IRegionManager regionManager) : base(regionManager)
		{
			this.BackCommand = new DelegateCommand(() => ExecuteBackCommand());
			this.SubmitCommand = new DelegateCommand(() => ExecuteSubmitCommand(),
				() => !string.IsNullOrEmpty(this.StoreName) && !string.IsNullOrEmpty(this.PhoneNumber) &&
				!string.IsNullOrEmpty(this.Street) && !string.IsNullOrEmpty(this.Address))
				.ObservesProperty(() => this.StoreName)
				.ObservesProperty(() => this.PhoneNumber)
				.ObservesProperty(() => this.Street)
				.ObservesProperty(() => this.Address);
		}

		
		private MasterStoreItemModel _masterStore;
		public MasterStoreItemModel MasterStore
		{
			get => _masterStore;
			set => SetProperty(ref _masterStore, value);
		}

		private string _storeName = string.Empty;
		public string StoreName
		{
			get => _storeName;
			set => SetProperty(ref _storeName, value);
		}

		private string _phoneNumber = string.Empty;
		public string PhoneNumber
		{
			get => _phoneNumber;
			set => SetProperty(ref _phoneNumber, value);
		}

		private string _street = string.Empty;
		public string Street
		{
			get => _street;
			set => SetProperty(ref _street, value);
		}

		private string _address = string.Empty;
		public string Address
		{
			get => _address;
			set => SetProperty(ref _address, value);
		}

		public DelegateCommand BackCommand { get; private set; }
		public DelegateCommand SubmitCommand { get; private set; }

		private void ExecuteBackCommand()
		{
			this.RegionNavigationService.Journal.CurrentEntry.Parameters.Add(NavigationConstants.MasterStoreModel, MasterStore);
			this.RegionNavigationService.Journal.GoBack();
		}

		private void ExecuteSubmitCommand()
		{
			this.MasterStore.StoreName = this.StoreName;
			this.MasterStore.PhoneNumber = this.PhoneNumber;
			this.MasterStore.Address = this.Address;
			this.MasterStore.Street = this.Street;

			var parameters = new NavigationParameters();
			parameters.Add(NavigationConstants.MasterStoreModel, this.MasterStore);
			this.RegionManager.RequestNavigate("ContentRegion", ViewNames.RegisterMasterStoreReviewPage, parameters);
		}


		public override void OnNavigatedTo(NavigationContext navigationContext)
		{
			base.OnNavigatedTo(navigationContext);

			if (navigationContext.Parameters.Any(x => x.Key == NavigationConstants.MasterStoreModel))
			{
				this.MasterStore = navigationContext.Parameters[NavigationConstants.MasterStoreModel] as MasterStoreItemModel;
			}
		}

	}
}
