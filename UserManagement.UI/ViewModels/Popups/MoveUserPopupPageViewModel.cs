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

            DeleteStoreUserCommand = new DelegateCommand(async () => await ExecuteDeleteStoreUserCommand());

            CancelCommand = new DelegateCommand(() => ExecuteCancelCommand());
            SubmitCommand = new DelegateCommand(async () => await ExecuteSubmitCommand(), () =>
            {
                int val = 0;
                return int.TryParse(EnteredRow, out val);
            }).ObservesProperty(() => EnteredRow);
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
            var parameter = StoreUsers[_selectedIndex];

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
                    MessageBox.Show(result.Message, "Unsuccessful");
                }
            }
            else if (result.StatusCode == (int)GenericStatusValue.NoInternetConnection)
            {
                MessageBox.Show(MessageBoxMessage.NoInternetConnection, "Unsuccessful");
            }
            else if (result.StatusCode == (int)GenericStatusValue.HasErrorMessage)
            {
                MessageBox.Show(((EntityBase) result).Message, "Unsuccessful");
            }
            else
            {
                MessageBox.Show(MessageBoxMessage.UnknownErorr, "Unsuccessful");
            }

            SetLoaderVisibility();
        }

        private void ExecuteCancelCommand()
        {
            RegionNavigationService.Journal.Clear();
            _eventAggregator.GetEvent<MoveStoreUserSubmitEvent>().Publish(null);
            //SetUnsetProperties();
        }

        private async Task ExecuteSubmitCommand()
        {
            var enteredRowIndex = Convert.ToInt32(EnteredRow) - 1;
            if (enteredRowIndex + 1 > StoreUsers.Count)
            {
                MessageBox.Show("this spot is greater than the list count.");
                return;
            }
            if (enteredRowIndex > 3) 
                if (enteredRowIndex != _selectedIndex)
                {

                    SetLoaderVisibility("Moving user position...");

                    var reqEntity = new MoveStoreUserRequestEntity
                    {
                        Mid = StoreUsers[_selectedIndex].Id,
                        OrderId = StoreUsers[_selectedIndex].OrderId,
                        OldCellNo = StoreUsers[_selectedIndex].DeliverOrderStatus,
                        MovedId = StoreUsers[enteredRowIndex].Id,
                        MovedPosOid = StoreUsers[enteredRowIndex].OrderId,
                        NewCellNo = StoreUsers[enteredRowIndex].DeliverOrderStatus
                    };

                    var result = await _windowsManager.MoveStoreUser(reqEntity);

                    SetLoaderVisibility();
                    switch (result.StatusCode)
                    {
                        case (int) GenericStatusValue.Success when Convert.ToBoolean(result.Status):
                            SetUnsetProperties();
                            _eventAggregator.GetEvent<MoveStoreUserSubmitEvent>().Publish(new MoveStoreUserItemModel());
                            break;
                        case (int) GenericStatusValue.Success:
                            MessageBox.Show(result.Message, "Unsuccessful");
                            break;
                        case (int) GenericStatusValue.NoInternetConnection:
                            MessageBox.Show(MessageBoxMessage.NoInternetConnection, "Unsuccessful");
                            break;
                        case (int) GenericStatusValue.HasErrorMessage:
                            MessageBox.Show(((EntityBase) result).Message, "Unsuccessful");
                            break;
                        default:
                            MessageBox.Show(MessageBoxMessage.UnknownErorr, "Unsuccessful");
                            break;
                    }

                    //logic
                }
                else
                
                    MessageBox.Show("You are already in this position");
                
            else
            
                MessageBox.Show("You can't move in this position.");
            
        }

        private void SetLoaderVisibility(string message = "")
        {
            LoaderMessage = message;

            LoaderVisibility = string.IsNullOrEmpty(message) ? Visibility.Collapsed : Visibility.Visible;
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

            if (storeUsers != null)  StoreUsers = storeUsers;
            

            _selectedIndex = Convert.ToInt32(navigationContext.Parameters[NavigationConstants.SelectedIndex].ToString());
            SelectedRow = _selectedIndex + 1;

            DisplayText = $"Please enter the position you would like to move the entry (Current Row{SelectedRow}) to: ";
        }
    }
}
