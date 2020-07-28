using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using UserManagement.Common.Constants;
using UserManagement.Common.Enums;
using UserManagement.Entity;
using UserManagement.Manager;
using UserManagement.UI.Events;
using UserManagement.UI.ItemModels;

namespace UserManagement.UI.ViewModels
{
    public class MoveUserPopupPageViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowsManager _windowsManager;

        public MoveUserPopupPageViewModel(IRegionManager regionManager,
            IEventAggregator eventAggregator, IWindowsManager windowsManager) : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _windowsManager = windowsManager;

            this.DeleteStoreUserCommand = new DelegateCommand(async () => await ExecuteDeleteStoreUserCommand());

            this.CancelCommand = new DelegateCommand(() => ExecuteCancelCommand());
            this.SubmitCommand = new DelegateCommand(async () => await ExecuteSubmitCommand(), () =>
            {
                int val = 0;
                return int.TryParse(this.EnteredRow, out val);
            }).ObservesProperty(() => this.EnteredRow);
        }

        private int _selectedIndex;
        private List<StoreUserEntity> _storeUsers;
        public List<StoreUserEntity> StoreUsers
        {
            get => _storeUsers;
            set => SetProperty(ref _storeUsers, value);
        }

        private long _selectedRow;
        public long SelectedRow
        {
            get => _selectedRow;
            set => SetProperty(ref _selectedRow, value);
        }

        private string _enteredRow;
        public string EnteredRow
        {
            get => _enteredRow;
            set => SetProperty(ref _enteredRow, value);
        }

        private string _displayText;
        public string DisplayText
        {
            get => _displayText;
            set => SetProperty(ref _displayText, value);
        }

        private Visibility _loaderVisibility = Visibility.Collapsed;
        public Visibility LoaderVisibility
        {
            get => _loaderVisibility;
            set => SetProperty(ref _loaderVisibility, value);
        }

        private string _loaderMessage = string.Empty;
        public string LoaderMessage
        {
            get => _loaderMessage;
            set => SetProperty(ref _loaderMessage, value);
        }

        public DelegateCommand DeleteStoreUserCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SubmitCommand { get; private set; }

        private async Task ExecuteDeleteStoreUserCommand()
        {
            var parameter = this.StoreUsers[_selectedIndex];

            SetLoaderVisibility("Moving user to archive...");

            var result = await _windowsManager.DeleteStoreUser(new DeleteStoreUserRequestEntity()
            {
                Id = parameter.Id,
                MasterStoreId = parameter.MasterStoreId,
                OrphanStatus = parameter.OrphanStatus,
                UserId = parameter.UserId,
                SuperMasterId = Config.MasterStore.UserId
            });

            SetLoaderVisibility();

            if (result.StatusCode == (int)GenericStatusValue.Success)
            {
                if (Convert.ToBoolean(result.Status))
                {
                    SetUnsetProperties();
                    _eventAggregator.GetEvent<MoveStoreUserToArchiveSubmitEvent>().Publish(new MoveStoreUserItemModel());
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

            SetLoaderVisibility();
        }

        private void ExecuteCancelCommand()
        {
            this.RegionNavigationService.Journal.Clear();
            _eventAggregator.GetEvent<MoveStoreUserSubmitEvent>().Publish(null);
            //SetUnsetProperties();
        }

        private async Task ExecuteSubmitCommand()
        {
            var enteredRowIndex = Convert.ToInt32(this.EnteredRow) - 1;

            if (enteredRowIndex > 3)
            {
                if (enteredRowIndex != _selectedIndex)
                {
                    if (enteredRowIndex + 1 > this.StoreUsers.Count)
                    {
                        MessageBox.Show("this spot is greater than the list count.");
                        return;
                    }

                    SetLoaderVisibility("Moving user postion...");

                    var reqEntity = new MoveStoreUserRequestEntity();
                    reqEntity.Mid = this.StoreUsers[_selectedIndex].Id;
                    reqEntity.OrderId = this.StoreUsers[_selectedIndex].OrderId;
                    reqEntity.OldCellNo = this.StoreUsers[_selectedIndex].DeliverOrderStatus;
                    reqEntity.MovedId = this.StoreUsers[enteredRowIndex].Id;
                    reqEntity.MovedPosOid = this.StoreUsers[enteredRowIndex].OrderId;
                    reqEntity.NewCellNo = this.StoreUsers[enteredRowIndex].DeliverOrderStatus;

                    var result = await _windowsManager.MoveStoreUser(reqEntity);

                    SetLoaderVisibility();
                    if (result.StatusCode == (int)GenericStatusValue.Success)
                    {
                        if (Convert.ToBoolean(result.Status))
                        {
                            SetUnsetProperties();
                            _eventAggregator.GetEvent<MoveStoreUserSubmitEvent>().Publish(new MoveStoreUserItemModel());
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

                    //logic
                }
                else
                {
                    MessageBox.Show("You are already in this position");
                }
            }
            else
            {
                MessageBox.Show("You can't move in this position.");
            }
        }

        private void SetLoaderVisibility(string message = "")
        {
            this.LoaderMessage = message;

            if (string.IsNullOrEmpty(message))
            {
                this.LoaderVisibility = Visibility.Collapsed;
            }
            else
            {
                this.LoaderVisibility = Visibility.Visible;
            }
        }

        private void SetUnsetProperties()
        {
            EnteredRow = string.Empty;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            SetUnsetProperties();

            var storeUsers = navigationContext.Parameters[NavigationConstants.StoreUsers] as List<StoreUserEntity>;

            if (storeUsers != null)
            {
                this.StoreUsers = storeUsers;
            }

            _selectedIndex = Convert.ToInt32(navigationContext.Parameters[NavigationConstants.SelectedIndex].ToString());
            this.SelectedRow = _selectedIndex + 1;

            this.DisplayText = $"Please enter the position you would like to move the entry (Current Row{this.SelectedRow}) to: ";
        }
    }
}
