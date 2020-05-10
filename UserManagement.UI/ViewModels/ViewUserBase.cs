using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UserManagement.Common.Constants;
using UserManagement.Common.Enums;
using UserManagement.Entity;
using UserManagement.Manager;
using UserManagement.UI.Converters;
using UserManagement.UI.ItemModels;
using UserManagement.UI.Views;

using MessageBox = System.Windows.MessageBox;


namespace UserManagement.UI.ViewModels
{
    public class ViewUserBase : Version4Buttons
    {
        private ObservableCollection<StoreUserEntity> _archiveStoreUsers = new ObservableCollection<StoreUserEntity>();
        private string _firstName;
        private string _lastName;
        private string _mobileNumber;
        private NonMobileUserItemModel _nonMobileUser;
        private ObservableCollection<StoreUserEntity> _storeUsers = new ObservableCollection<StoreUserEntity>();
        private PopupVisibilityEvent ev;
        private string _roomNumber;
        private ObservableCollection<StoreUserEntity> _assignedUsers;
        private bool UnregisteredMobileNumber { get; set; }

        public ViewUserBase(IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IWindowsManager windowsManager) : base(regionManager, eventAggregator, windowsManager)
        {
        }

        public ObservableCollection<StoreUserEntity> ArchieveStoreUsers
        {
            get => _archiveStoreUsers;
            set => SetProperty(ref _archiveStoreUsers, value);
        }

        public ObservableCollection<StoreUserEntity> StoreUsers
        {
            get => _storeUsers;
            set => SetProperty(ref _storeUsers, value);
        }

        protected int TotalStoreUsers => StoreUsers == null || StoreUsers.Count <= 0 ? 0 : StoreUsers.Count;

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public string MobileNumber
        {
            get => _mobileNumber;
            set => SetProperty(ref _mobileNumber, value);
        }

        public string RoomNumber
        {
            get => _roomNumber;
            set => SetProperty(ref _roomNumber, value);
        }

        public DelegateCommand<StoreUserEntity> MoveStoreUserCommand { get; private set; }

        public DelegateCommand NonMobileUserCommand { get; private set; }
        public DelegateCommand<StoreUserEntity> SetRoomNumberCommand { get; private set; }
        public ObservableCollection<StoreUserEntity> AssignedUsers
        {
            get => _assignedUsers;
            set => SetProperty(ref _assignedUsers, value);
        }
        private NonMobileUserItemModel NonMobileUser
        {
            get => _nonMobileUser;
            set => SetProperty(ref _nonMobileUser, value);
        }

        private UserDetailsPage userDetailsPage { get; set; }
        private RegisterUserPopupPage RegisterUserPage { get; set; }
        public DelegateCommand<StoreUserEntity> EditAppointmentCommand { get; private set; }
        public DelegateCommand<StoreUserEntity> DeleteStoreUserCommand { get; private set; }
        public DelegateCommand<StoreUserEntity> DeleteArchiveUserCommand { get; private set; }
        public DelegateCommand<StoreUserEntity> EditNonMobileArchiveStoreUserCommand { get; private set; }
        public DelegateCommand<StoreUserEntity> EditNonMobileStoreUserCommand { get; private set; }
        public DelegateCommand<StoreUserEntity> EditStoreUserCommand { get; private set; }
        public DelegateCommand<StoreUserEntity> EditStoreMobileUserCommand { get; private set; }

        public DelegateCommand<StoreUserEntity> ArchiveIDCheckedCommand { get; private set; }
        public DelegateCommand<StoreUserEntity> UserDetailWindowCommand { get; private set; }

        public DelegateCommand<string> ActCommand { get; private set; }


        private bool CheckUser()
        {
            var msg = new List<string>();
            if (string.IsNullOrEmpty(FirstName)) msg.Add("First Name is required.");
            if (string.IsNullOrEmpty(LastName)) msg.Add("Last Name is required.");
            if (string.IsNullOrEmpty(MobileNumber)) msg.Add("Mobile Number is required.");
            if (msg.Count == 0) return true;

            var s = "";
            foreach (var t in msg)
                s = s + t + Environment.NewLine;
            MessageBox.Show(s, "Required");
            return false;
        }

        private async Task DeleteArchiveUser(StoreUserEntity parameter)
        {
            SetLoaderVisibility("Deleting Archive User...");

            var deleteResult = await _windowsManager.DeleteArchiveUser(new DeleteArchiveUserRequestEntity()
            {
                Id = parameter.Id,
                UserId = parameter.UserId,
                MasterStoreId = parameter.MasterStoreId,
                SuperMasterId = Config.MasterStore.UserId
            });

            switch (deleteResult.StatusCode)
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
                    MessageBox.Show(((EntityBase)deleteResult).Message, "Unsuccessful");
                    break;

                default:
                    SetLoaderVisibility();
                    MessageBox.Show(MessageBoxMessage.UnknownErorr, "Unsuccessful");
                    break;
            }
        }

        private async Task ExecuteArchiveIDCheckedCommand(StoreUserEntity parameter)
        {
            var dialogResult = MessageBox.Show("Id has not been checked? (Select Yes if you want Id Checked)",
                "ID Required", MessageBoxButton.YesNo);

            if (dialogResult == MessageBoxResult.Yes)
            {
                SetLoaderVisibility("Updating IDR...");

                var idrResult = await _windowsManager.CheckIDRArchiveUser(new ManageUserRequestEntity()
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
                        MessageBox.Show(((EntityBase)idrResult).Message, "Unsuccessful");
                        break;

                    default:
                        SetLoaderVisibility();
                        MessageBox.Show(MessageBoxMessage.UnknownErorr, "Unsuccessful");
                        break;
                }

                SetLoaderVisibility();
            }
        }

        private async Task ExecuteDeleteArchiveUserCommand(StoreUserEntity parameter)
        {
            if (parameter.OrphanStatus == "1")

                if (parameter.IdrStatus == "0")

                    if (parameter.RegisterType == "second")
                    {
                        var dialogResult =
                            MessageBox.Show("Id has not been checked? (Select Yes if you want Id Checked)", "",
                                MessageBoxButton.YesNo);
                        if (dialogResult == MessageBoxResult.Yes) await ManageUser(parameter);
                    }
                    else
                    {
                        var dialogResult =
                            MessageBox.Show(
                                "This person was not verified. If you delete them, they will be not be registered. Delete anyway?",
                                "Delete", MessageBoxButton.YesNo);
                        if (dialogResult == MessageBoxResult.Yes) await DeleteArchiveUser(parameter);
                    }
                else
                {
                    var dialogResult = MessageBox.Show("Are you sure you want to delete?", "Delete",
                        MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes) await DeleteArchiveUser(parameter);
                }
            else
            {
                var dialogResult =
                    MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) await DeleteArchiveUser(parameter);
            }
        }

        private async Task ExecuteDeleteStoreUserCommand(StoreUserEntity parameter)
        {
            var dialogResult = MessageBox.Show("Are you want to delete a user?", "Delete store user",
                MessageBoxButton.YesNo);

            if (dialogResult == MessageBoxResult.Yes)
            {
                SetLoaderVisibility("Deleting user...");

                var result = await _windowsManager.DeleteStoreUser(new DeleteStoreUserRequestEntity()
                {
                    Id = parameter.Id,
                    MasterStoreId = parameter.MasterStoreId,
                    OrphanStatus = parameter.OrphanStatus,
                    UserId = parameter.UserId,
                    SuperMasterId = Config.MasterStore.UserId
                });

                switch (result.StatusCode)
                {
                    case (int)GenericStatusValue.Success:
                        {
                            SetLoaderVisibility();
                            if (Convert.ToBoolean(result.Status))
                            {
                                ResetFields();
                                await GetData();
                            }
                            else MessageBox.Show(result.Message, "Unsuccessful");

                            break;
                        }
                    case (int)GenericStatusValue.NoInternetConnection:
                        SetLoaderVisibility();
                        MessageBox.Show(MessageBoxMessage.NoInternetConnection, "Unsuccessful");
                        break;

                    case (int)GenericStatusValue.HasErrorMessage:
                        SetLoaderVisibility();
                        MessageBox.Show(((EntityBase)result).Message, "Unsuccessful");
                        break;

                    default:
                        SetLoaderVisibility();
                        MessageBox.Show(MessageBoxMessage.UnknownErorr, "Unsuccessful");
                        break;
                }

                SetLoaderVisibility();
            }
        }

        private void ExecuteEditAppointmentUserCommand(StoreUserEntity user)
        {
            _eventAggregator.GetEvent<PopupVisibilityEvent>().Publish(true);
            var parameters = new NavigationParameters
            {
                {NavigationConstants.SelectedStoreUser, user},
                {NavigationConstants.Action, "update_non_mobile"},
                {NavigationConstants.IsSelectedStoreUser, true}
            };
            RegionManager.RequestNavigate("PopupRegion", ViewNames.EditAppointmentPopupPage, parameters);
        }

        private void ExecuteEditNonMobileArchiveStoreUserCommand(StoreUserEntity user)
        {
            if (user.OrphanStatus != "1") return;
            _eventAggregator.GetEvent<PopupVisibilityEvent>().Publish(true);
            var parameters = new NavigationParameters
            {
                {NavigationConstants.SelectedStoreUser, user},
                {NavigationConstants.Action, "update_non_mobile_archive"}
            };
            RegionManager.RequestNavigate("PopupRegion", ViewNames.UpdateNonMobileUserPopupPage, parameters);
        }

        private void ExecuteEditNonMobileStoreUserCommand(StoreUserEntity user)
        {
            if (user.OrphanStatus != "1") return;
            _eventAggregator.GetEvent<PopupVisibilityEvent>().Publish(true);
            var parameters = new NavigationParameters
            {
                {NavigationConstants.SelectedStoreUser, user},
                {NavigationConstants.Action, "update_non_mobile"}
            };
            RegionManager.RequestNavigate("PopupRegion", ViewNames.UpdateNonMobileUserPopupPage, parameters);
        }

        private void ExecuteRegisterUserCommand(StoreUserEntity user)
        {
            ev = _eventAggregator.GetEvent<PopupVisibilityEvent>();
            ev.Publish(true);
            var parameters = new NavigationParameters
            {
                { NavigationConstants.SelectedStoreUser, user }
            };

            RegionManager.RequestNavigate
                ("PopupRegion", ViewNames.RegisterUserPopupPage, parameters);
        }


        private void ExecuteEditStoreUserCommand(StoreUserEntity user)
        {
            _eventAggregator.GetEvent<PopupVisibilityEvent>().Publish(true);
            var parameters = new NavigationParameters { { NavigationConstants.SelectedStoreUser, user } };
            RegionManager.RequestNavigate("PopupRegion", ViewNames.EditUserPopupPage, parameters);
        }

        private void ExecuteMoveStoreUserCommand(StoreUserEntity user)
        {
            var reverseStoreUsers = StoreUsers.ToList();
            reverseStoreUsers.Reverse();

            if (reverseStoreUsers.Count >= 5)
            {
                var selectedIndex = reverseStoreUsers.IndexOf(user);

                if (selectedIndex > 3)
                {
                    var dialogResult = MessageBox.Show("Are you sure you want to move this entry's position?", "Moving user position", MessageBoxButton.YesNo);

                    if (dialogResult != MessageBoxResult.Yes) return;
                    _eventAggregator.GetEvent<PopupVisibilityEvent>().Publish(true);
                    var parameters = new NavigationParameters
                    {
                        {NavigationConstants.StoreUsers, reverseStoreUsers.ToList()},
                        {NavigationConstants.SelectedIndex, selectedIndex}
                    };
                    RegionManager.RequestNavigate("PopupRegion", ViewNames.MoveUserPopupPage, parameters);
                }
                else MessageBox.Show("Sorry you can't move once in here.");
            }
            else MessageBox.Show("Sorry you can't move once in here.");
        }

        private void ExecuteNonMobileUserCommand()
        {
            _eventAggregator.GetEvent<PopupVisibilityEvent>().Publish(true);
            var parameters = new NavigationParameters();
            RegionManager.RequestNavigate("PopupRegion", ViewNames.NonMobileUserPopup, parameters);
        }

        protected async Task ExecuteRefreshDataCommand()
        {
            SetLoaderVisibility("Loading data...");
            ResetFields();
            await GetData();
        }
        private void ExecuteSetRoomNumberCommand(StoreUserEntity user)
        {
            _eventAggregator.GetEvent<PopupVisibilityEvent>().Publish(true);
            var parameters = new NavigationParameters { { NavigationConstants.SelectedStoreUser, user } };

            RegionManager.RequestNavigate("PopupRegion", ViewNames.SetRoomNumberPopUpPage, parameters);

        }
        private void ExecuteUserDetailWindowCommand(StoreUserEntity user)
        {
            if (userDetailsPage != null)
            {
                userDetailsPage.Close();
                userDetailsPage = null;
            }

            userDetailsPage = new UserDetailsPage
            {
                DataContext = user,
                PostalCodeText = { Text = user.IsZipCode ? "Zip Code :" : "Postal Code :" }
            };
            userDetailsPage.Show();
        }

        protected async Task GetArchiveStoreUsers()
        {
            var result = await _windowsManager.GetArchiveStoreUsers(new GetStoreUsersRequestEntity()
            {
                StoreId = Config.MasterStore.StoreId,
                SuperMasterId = Config.MasterStore.UserId,
                TimeZone = Config.MasterStore.TimeZone
            });

            ArchieveStoreUsers = new ObservableCollection<StoreUserEntity>();

            if (result.StatusCode == (int)GenericStatusValue.Success)
                if (Convert.ToBoolean(result.Status))
                {
                    result.Data = result.Data.Where(x => x != null && !string.IsNullOrEmpty(x.Id)).ToList();
                    ArchieveStoreUsers = new ObservableCollection<StoreUserEntity>(result.Data);
                }

            GetArchiveUsers();
        }

        private void GetArchiveUsers()
        {
            if (ArchieveStoreUsers != null && ArchieveStoreUsers.Count > 0)

                if (ArchieveStoreUsers.Any(a => a.RegType.Equals("Express")))

                    if (ArchieveStoreUsers.Any(a => a.TimeDifference.Equals("early")) ||
                        ArchieveStoreUsers.Any(a => a.TimeDifference.Equals("ready")))

                        StartStopExpressTimer();
        }

        protected override async Task GetData()
        {
            try
            {
                await base.GetData();
                await Task.WhenAll(GetStoreUsers());
                await Task.WhenAll(GetArchiveStoreUsers());
            }
            //ToDO: fix this exception
            catch
            {
                if (!UpdateExpressTime.Enabled) UpdateExpressTime.Start();
            }
            finally
            {
                LoaderVisibility = Visibility.Collapsed;
            }
        }

        private void GetNonMobileUserInfo(SaveUserDataRequestEntity reqEntity)
        {
            Debug.Assert(NonMobileUser != null);
            reqEntity.Mobile = string.Empty;
            reqEntity.OrphanStatus = 1;
            reqEntity.PostalCode = NonMobileUser.PostalCode;
            reqEntity.HomePhone = NonMobileUser.HomePhone;
            reqEntity.Country = NonMobileUser.Country;
            reqEntity.City = NonMobileUser.City;
            reqEntity.State = NonMobileUser.State;
            reqEntity.Gender = NonMobileUser.Gender;
            reqEntity.DOB = NonMobileUser.DOB;
        }

        protected async Task GetStoreUsers()
        {
            var result = await _windowsManager.GetStoreUsers(new GetStoreUsersRequestEntity()
            {
                StoreId = Config.MasterStore.StoreId,
                SuperMasterId = Config.MasterStore.UserId
            });

            StoreUsers = new ObservableCollection<StoreUserEntity>();

            if (result.StatusCode == (int)GenericStatusValue.Success)
            {
                if (Convert.ToBoolean(result.Status))
                {
                    result.Data = result.Data.Where(x => x != null && !string.IsNullOrEmpty(x.Id)).ToList();

                    if (result.Data.Count > 0)
                    {
                        //The bottom 4 rows of the table are yellow (like pouring of water).
                        var list = result.Data.Skip(Math.Max(0, result.Data.Count() - 4)).ToList();
                        list.ForEach(s => s.Column2RowColor = ColorNames.Yellow);

                        if (result.Data.Count > 4)
                        {
                            var takeSecondFour = result.Data.Count <= 8 ? result.Data.Count() - 4 : 4;

                            //And as you pour more water, rows 5, 6, 7, and 8 will be blue.
                            result.Data.Skip(
                                Math.Max(0, result.Data.Count() - 8)
                            ).Take(takeSecondFour).ToList().ForEach(s => s.Column2RowColor = ColorNames.Blue);
                        }

                        //And as you pour more rows 9 and above all the way to infinity are green.
                        //NOTE: We have set the green color as a default color so no needed for code.
                    }

                    StoreUsers = new ObservableCollection<StoreUserEntity>(result.Data);
                }
            }
        }

        protected void GetUserName(SaveUserDataRequestEntity reqEntity)
        {
            reqEntity.FirstName = FirstName;
            reqEntity.LastName = LastName;
        }

        protected async Task<bool> HandeArchiveUsers()
        {
            if (ArchieveStoreUsers == null ||
                ArchieveStoreUsers.Count <= 0 ||
                !ArchieveStoreUsers.Any(a => a.RegType.Equals("Express"))) return true;
            {
                if (ArchieveStoreUsers.Any(a => a.TimeDifference.Equals("early")) ||
                    ArchieveStoreUsers.Any(a => a.TimeDifference.Equals("ready")))

                    await Task.WhenAll(GetArchiveStoreUsers());
            }
            return false;
        }

        private async Task ManageUser(StoreUserEntity parameter)
        {
            SetLoaderVisibility("Deleting Archive User...");

            var deleteResult = await _windowsManager.ManageUser(new ManageUserRequestEntity()
            {
                Id = parameter.Id,
                MasterStoreId = parameter.MasterStoreId
            });

            switch (deleteResult.StatusCode)
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
                    MessageBox.Show(((EntityBase)deleteResult).Message, "Unsuccessful");
                    break;

                default:
                    SetLoaderVisibility();
                    MessageBox.Show(MessageBoxMessage.UnknownErorr, "Unsuccessful");
                    break;
            }
        }

        private new void ResetFields()
        {
            base.ResetFields();
            ResetUser();
        }

        private void ResetUser()
        {
            NonMobileUser = null;
            FirstName = string.Empty;
            LastName = string.Empty;
            MobileNumber = string.Empty;
        }

        private async Task SaveUser(SaveUserDataRequestEntity reqEntity, bool Dummy)
        {
            var result = await _windowsManager.SaveUserData(reqEntity, Dummy);

            switch (result.StatusCode)
            {
                case (int)GenericStatusValue.Success:
                    {

                        SetLoaderVisibility();

                        if (Convert.ToBoolean(result.Status))

                        {

                            if (StoreUsers.Count < 4 ||
                                !string.IsNullOrEmpty(reqEntity.ExpressTime))
                                await GetData();
                        }
                        else
                        {
                            if ((result.Message == "Mobile no doesnot exits!") ||
                                (result.Message == "Name doesnot match!"))
                                NewMobileUser = true;
                            else
                                MessageBox.Show(result.Message, "Unsuccessful");
                        }

                        break;
                    }
                case (int)GenericStatusValue.NoInternetConnection:
                    SetLoaderVisibility();
                    MessageBox.Show(MessageBoxMessage.NoInternetConnection, "Unsuccessful");
                    break;

                case (int)GenericStatusValue.HasErrorMessage:
                    SetLoaderVisibility();
                    MessageBox.Show(((EntityBase)result).Message, "Unsuccessful");
                    break;

                default:
                    SetLoaderVisibility();
                    MessageBox.Show(MessageBoxMessage.UnknownErorr, "Unsuccessful");
                    break;
            }
        }

        public bool NewMobileUser { get; set; } = false;

        private SaveUserDataRequestEntity GetSaveUserDataRequestEntity(bool isMobileUser)
        {
            var reqEntity = new SaveUserDataRequestEntity { Action = "master" };


            GetUserName(reqEntity);

            reqEntity.CountryCode = Config.MasterStore.CountryCode;
            reqEntity.StoreId = Config.MasterStore.StoreId;
            reqEntity.SuperMasterId = Config.MasterStore.UserId;

            if (isMobileUser)
                SetMobileUserData(reqEntity);
            else
                GetNonMobileUserInfo(reqEntity);


            reqEntity.ExpressTime = ExpressTime;
            reqEntity.DeliverOrderStatus = TotalStoreUsers;
            reqEntity.FillStatus = 1;
            return reqEntity;
        }

        private void SetMobileUserData(SaveUserDataRequestEntity reqEntity)
        {
            reqEntity.Mobile = MobileNumber;
            reqEntity.OrphanStatus = 0;
            reqEntity.PostalCode = string.Empty;
            reqEntity.HomePhone = string.Empty;
        }

        protected void SetUser(NonMobileUserItemModel user)
        {
            NonMobileUser = user;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }


        protected async Task ExecuteActCommand(string cmd)
        {

            switch (cmd)
            {
                case "Non-Mobile": ExecuteNonMobileUserCommand(); break;
                case "Exp": ExecuteExpressUserCommand(); break;
                case "Add":

                    await ExecuteAddUserCommand(false);
                    if (NewMobileUser)
                    {
                        // ask to Register
                        var message = $"Mobile Number: {MobileNumber} not found. {Environment.NewLine} Do you want to register a new user?";
                        const string title = "Mobile Number Not Found";

                        var result = MessageBox.Show(message, title, MessageBoxButton.OKCancel, MessageBoxImage.Question);
                        if (result == MessageBoxResult.OK)
                        {
                            await ExecuteAddUserCommand(true);
                            var ent = new StoreUserEntity
                            {
                                Firstname = FirstName,
                                Lastname = LastName,
                                Mobile = MobileNumber,
                                RoomNumber = RoomNumber
                            };
                            ExecuteRegisterUserCommand(ent);

                            NewMobileUser = false;

                            await ExecuteAddUserCommand(false);

                        }
                        else
                        {
                            NewMobileUser = false;
                        }
                    }

                    else
                        ResetFields();

                    break;
                case "EXP":
                    ExecuteExpressUserCommand();
                    break;
                case "Refresh":
                    await ExecuteRefreshDataCommand();
                    break;
                default:
                    Debug.Assert(false, $"bad command: {cmd}");
                    logger.Error($"bad command: {cmd}");
                    break;

            }
        }

        protected async Task ExecuteAddUserCommand(bool Dummy)
        {
            CanTapAddCommand = false;
            var isMobileUser = (NonMobileUser == null);
            if (!CheckUser()) return;

            if (!CheckButtonsExpress()) return;

            var reqEntity = GetSaveUserDataRequestEntity(isMobileUser);

            SetAppButtons(reqEntity);

            SetLoaderVisibility("Adding user...");

            await SaveUser(reqEntity, Dummy);

            SetLoaderVisibility();
            CanTapAddCommand = true;
            ExpressTime = string.Empty;
        }

        private void ExecuteExpressUserCommand()
        {
            if (!CheckUser()) return;

            if (!CheckButtonsExpress()) return;

            _eventAggregator.GetEvent<PopupVisibilityEvent>().Publish(true);
            RegionManager.RequestNavigate("PopupRegion", ViewNames.ExpressTimePickerPopupPage);
        }

        protected new void SetDelegateCommands()
        {
            base.SetDelegateCommands();

            EditAppointmentCommand = new DelegateCommand<StoreUserEntity>(ExecuteEditAppointmentUserCommand);
            ArchiveIDCheckedCommand = new DelegateCommand<StoreUserEntity>(async (user) => await ExecuteArchiveIDCheckedCommand(user));
            UserDetailWindowCommand = new DelegateCommand<StoreUserEntity>(ExecuteUserDetailWindowCommand);
            ActCommand = new DelegateCommand<string>(async (cmd) => await ExecuteActCommand(cmd));

            NonMobileUserCommand = new DelegateCommand(ExecuteNonMobileUserCommand);
            //    AddUserCommand = new DelegateCommand(async () => await ExecuteAddUserCommand());
            DeleteStoreUserCommand = new DelegateCommand<StoreUserEntity>(async (user) => await ExecuteDeleteStoreUserCommand(user));
            DeleteArchiveUserCommand = new DelegateCommand<StoreUserEntity>(async (user) => await ExecuteDeleteArchiveUserCommand(user));

            //  ExpressUserCommand = new DelegateCommand(ExecuteExpressUserCommand);

            EditStoreUserCommand = new DelegateCommand<StoreUserEntity>(ExecuteEditStoreUserCommand);
            SetRoomNumberCommand = new DelegateCommand<StoreUserEntity>(ExecuteSetRoomNumberCommand);

            EditStoreMobileUserCommand = new DelegateCommand<StoreUserEntity>(ExecuteRegisterUserCommand);

            EditNonMobileStoreUserCommand = new DelegateCommand<StoreUserEntity>(ExecuteEditNonMobileStoreUserCommand);

            EditNonMobileArchiveStoreUserCommand = new DelegateCommand<StoreUserEntity>(ExecuteEditNonMobileArchiveStoreUserCommand);

            MoveStoreUserCommand = new DelegateCommand<StoreUserEntity>(ExecuteMoveStoreUserCommand);
        }
    }
}