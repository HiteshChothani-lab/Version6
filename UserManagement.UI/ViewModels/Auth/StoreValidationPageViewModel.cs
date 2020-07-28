using Prism.Commands;
using Prism.Regions;
using System;
using System.Threading.Tasks;
using System.Windows;
using UserManagement.Common.Constants;
using UserManagement.Common.Enums;
using UserManagement.Manager;
using UserManagement.UI.ItemModels;

namespace UserManagement.UI.ViewModels
{
	public class StoreValidationPageViewModel : ViewModelBase
	{
		private readonly IWindowsManager _windowsManager;
		public StoreValidationPageViewModel(IRegionManager regionManager, IWindowsManager windowsManager) : base(regionManager)
		{
			_windowsManager = windowsManager;

			this.SubmitCommand = new DelegateCommand(async () => await ExecuteSubmitCommand(),
				() => !string.IsNullOrEmpty(this.Username) && !string.IsNullOrEmpty(this.AccessCode) && this.CanExecuteSubmitCommand)
				.ObservesProperty(() => this.Username)
				.ObservesProperty(() => this.AccessCode)
				.ObservesProperty(() => this.CanExecuteSubmitCommand);
		}

		private string _username;
		public string Username
		{
			get => _username;
			set => SetProperty(ref _username, value);
		}

		private string _accessCode;
		public string AccessCode
		{
			get => _accessCode;
			set => SetProperty(ref _accessCode, value);
		}

		private bool _canExecuteSubmitCommand = true;
		public bool CanExecuteSubmitCommand
		{
			get => _canExecuteSubmitCommand;
			set => SetProperty(ref _canExecuteSubmitCommand, value);
		}
		public DelegateCommand SubmitCommand { get; private set; }

		private async Task ExecuteSubmitCommand()
		{
			this.CanExecuteSubmitCommand = false;

			var result = await _windowsManager.ValidateUser(new Entity.ValidateUserRequestEntity()
			{
				Username = Username,
				AccessCode = AccessCode
			});

			if (result.StatusCode == (int)GenericStatusValue.Success)
			{
				if (Convert.ToBoolean(result.Status))
				{
					var parameters = new NavigationParameters();
					parameters.Add(NavigationConstants.MasterStoreModel, new MasterStoreItemModel() { UserId = result.UserId });
					this.RegionManager.RequestNavigate("ContentRegion", ViewNames.RegisterMasterStore1Page, parameters);
				}
				else
				{
					MessageBox.Show(result.Messagee, "Unsuccessful");
				}
			}
			else if (result.StatusCode == (int)GenericStatusValue.NoInternetConnection)
			{
				MessageBox.Show(MessageBoxMessage.NoInternetConnection, "Unsuccessful");
			}
			else if (result.StatusCode == (int)GenericStatusValue.HasErrorMessage)
			{
				MessageBox.Show(result.Message, "Unsuccessful");
			}
			else
			{
				MessageBox.Show(MessageBoxMessage.UnknownErorr, "Unsuccessful");
			}

			this.CanExecuteSubmitCommand = true;
		}

		public override void OnNavigatedTo(NavigationContext navigationContext)
		{
			base.OnNavigatedTo(navigationContext);

#if DEBUG
			this.Username = "Store4";
			this.AccessCode = "111111";
#endif
		}
	}
}
