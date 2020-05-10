using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using NLog;
using UserManagement.Common.Constants;
using UserManagement.Common.Enums;
using UserManagement.Entity;
using UserManagement.Manager;
using UserManagement.Pushers.Events;
using UserManagement.UI.Converters;
using UserManagement.UI.Events;


namespace UserManagement.UI.ViewModels
{
    public class MainPageViewModel : ViewUserBase
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public MainPageViewModel(
                    IRegionManager regionManager,
                    IEventAggregator eventAggregator,
                    IWindowsManager windowsManager) : base(regionManager, eventAggregator,windowsManager)
        {
            
            setEventAggrivator();

            SetDelegateCommands();
            
            UpdateExpressTime.Elapsed += UpdateExpressTime_Elapsed;

            SetEvents();

        }

        private void SetEvents()
        {
            _eventAggregator.GetEvent<RefreshData>().Subscribe((model) =>
            {
                try
                {
                    lock (locker)
                    {
                        switch (model.Action)
                        {
                            case PusherAction.Store when model.EventName.Equals(PusherData.DeleteStoreUser):
                                Task.WaitAll(GetData());
                                break;
                            case PusherAction.Store:
                                Task.WhenAll(GetStoreUsers());
                                break;
                            case PusherAction.Archieve:
                                Task.WhenAll(GetArchiveStoreUsers());
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
                //ToDO: fix this exception
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            });
        }

        private void setEventAggrivator()
        {
            _eventAggregator.GetEvent<NonMobileUserUpdateEvent>().Subscribe(async (user) =>
            {
                _eventAggregator.GetEvent<PopupVisibilityEvent>().Publish(false);

                if (user == null) return;
                if (user.IsNewRecord) SetUser(user);
                else await GetData();
            });

            _eventAggregator.GetEvent<NonMobileUserEditEvent>().Subscribe(async (user) =>
            {
                _eventAggregator.GetEvent<PopupVisibilityEvent>().Publish(false);
                if (user != null) await GetData();
            });

            _eventAggregator.GetEvent<EditStoreUserSubmitEvent>().Subscribe(async (user) =>
            {
                
                _eventAggregator.GetEvent<PopupVisibilityEvent>().Publish(false);
                if (user != null) await GetData();
            });

            _eventAggregator.GetEvent<SetRoomNumberSubmitEvent>().Subscribe(async (room) =>
            {
                _eventAggregator.GetEvent<PopupVisibilityEvent>().Publish(false);
      
            });
            _eventAggregator.GetEvent<MoveStoreUserSubmitEvent>().Subscribe(async (user) =>
            {
                _eventAggregator.GetEvent<PopupVisibilityEvent>().Publish(false);
                if (user != null) await GetData();
            });

            _eventAggregator.GetEvent<MoveStoreUserToArchiveSubmitEvent>().Subscribe(async (user) =>
            {
                _eventAggregator.GetEvent<PopupVisibilityEvent>().Publish(false);
                if (user != null) await GetData();
            });

            _eventAggregator.GetEvent<EditUserAgeOrNeedleSubmitEvent>().Subscribe(async (user) =>
            {
                _eventAggregator.GetEvent<PopupVisibilityEvent>().Publish(false);
                if (user != null) await GetData();
            });

            _eventAggregator.GetEvent<ExpressTimeSubmitEvent>().Subscribe(async (expTime) =>
            {
                _eventAggregator.GetEvent<PopupVisibilityEvent>().Publish(false);
                if (expTime == null) return;

                ExpressTime = expTime.FinalTime;
                await ExecuteAddUserCommand(false);
            });
        }

        private new void SetDelegateCommands()
        {
            base.SetDelegateCommands();

            SetFlagCommand = new DelegateCommand<StoreUserEntity>(ExecuteFlagCommand);
            LogoutCommand = new DelegateCommand(ExecuteLogoutCommand);
            RefreshDataCommand = new DelegateCommand(async () => await ExecuteRefreshDataCommand());
            StoreIDCheckedCommand = new DelegateCommand<StoreUserEntity>(async (user) => await ExecuteStoreIDCheckedCommand(user));
        }
        
        public DelegateCommand LogoutCommand { get; private set; }

        public DelegateCommand RefreshDataCommand { get; private set; }

        public DelegateCommand<StoreUserEntity> SetFlagCommand { get; private set; }

        public DelegateCommand<StoreUserEntity> StoreIDCheckedCommand { get; private set; }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            try
            {
                await GetData();
            }
            //ToDO: fix this exception
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, "getdata", ex);
            }
        }

        private async void ExecuteFlagCommand(StoreUserEntity user)
        {
            try
            {
                SetLoaderVisibility("Updating Flag...");

                var result = await _windowsManager.SetUnsetFlag(new SetUnsetFlagRequestEntity()
                {
                    Id = user.Id,
                    MasterStoreId = user.MasterStoreId,
                    RecentStatus = user.IsFlagSet ? 0 : 1
                });

                switch (result.StatusCode)
                {
                    case (int)GenericStatusValue.Success when Convert.ToBoolean(result.Status):
                        await GetStoreUsers();
                        break;
                    case (int)GenericStatusValue.Success:
                        MessageBox.Show(result.Message, "Unsuccessful");
                        break;
                    case (int)GenericStatusValue.NoInternetConnection:
                        MessageBox.Show(MessageBoxMessage.NoInternetConnection, "Unsuccessful");
                        break;
                    case (int)GenericStatusValue.HasErrorMessage:
                        MessageBox.Show(((EntityBase) result).Message, "Unsuccessful");
                        break;
                    default:
                        MessageBox.Show(MessageBoxMessage.UnknownErorr, "Unsuccessful");
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                MessageBox.Show(ex.Message, "Unsuccessful");
            }
            finally
            {
                SetLoaderVisibility();
            }
        }

        private void ExecuteLogoutCommand()
        {
            _windowsManager.Logout();
            UpdateExpressTime.Stop();
            UpdateExpressTime.Elapsed -= UpdateExpressTime_Elapsed;
            UpdateExpressTime.Dispose();
            Application.Current.Shutdown();
        }

        private async Task ExecuteStoreIDCheckedCommand(StoreUserEntity parameter)
        {
            var dialogResult = MessageBox.Show("Id has not been checked? (Select Yes if you want Id Checked)", "ID Required", MessageBoxButton.YesNo);

            if (dialogResult == MessageBoxResult.Yes)
            {
                SetLoaderVisibility("Updating IDR...");

                var idrResult = await _windowsManager.CheckIDRStoreUser(new ManageUserRequestEntity()
                {
                    Id = parameter.Id,
                    MasterStoreId = parameter.MasterStoreId
                });

                switch (idrResult.StatusCode)
                {
                    case (int)GenericStatusValue.Success:
                        SetLoaderVisibility();
                        await GetData();
                        break;
                    case (int)GenericStatusValue.NoInternetConnection:
                        SetLoaderVisibility();
                        MessageBox.Show(MessageBoxMessage.NoInternetConnection, "Unsuccessful");
                        break;
                    case (int)GenericStatusValue.HasErrorMessage:
                        SetLoaderVisibility();
                        MessageBox.Show(((EntityBase) idrResult).Message, "Unsuccessful");
                        break;
                    default:
                        SetLoaderVisibility();
                        MessageBox.Show(MessageBoxMessage.UnknownErorr, "Unsuccessful");
                        break;
                }

                SetLoaderVisibility();
            }
        }

        protected override async Task GetData()
        {
           await base.GetData();

        }

        protected new void ResetFields()
        {
            base.ResetFields();
        }

        private async void UpdateExpressTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                UpdateExpressTime.Stop();
                if (await HandeArchiveUsers()) return;
            }
            catch
            {
                if (!UpdateExpressTime.Enabled) UpdateExpressTime.Start();
            }
        }
    }
}
