﻿using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Threading.Tasks;
using System.Windows;
using UserManagement.Common.Constants;
using UserManagement.Common.Enums;
using UserManagement.Entity;
using UserManagement.Manager;
using UserManagement.UI.Events;
using UserManagement.UI.ItemModels;
using UserManagement.UI.Views;

namespace UserManagement.UI.ViewModels
{
    public class EditButtonsPopupPageViewModel : ViewModelBase
    {
        private readonly ILocationManager _locationManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowsManager _windowsManager;

        public EditButtonsPopupPageViewModel(IRegionManager regionManager, IEventAggregator eventAggregator,
            IWindowsManager windowsManager, ILocationManager locationManager) : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _windowsManager = windowsManager;
            _locationManager = locationManager;

            CancelCommand = new DelegateCommand(() => ExecuteCancelCommand());
            SubmitCommand = new DelegateCommand(async () => await ExecuteSubmitCommand());
        }


        private PackAndShipPage packAndShipPage;
        private bool IsSelectedStoreUser = false;

        private StoreUserEntity _selectedStoreUser;
        public StoreUserEntity SelectedStoreUser
        {
            get => _selectedStoreUser;
            set => SetProperty(ref _selectedStoreUser, value);
        }

        private bool _isUserTypeMobile = true;
        public bool IsUserTypeMobile
        {
            get => _isUserTypeMobile;
            set => SetProperty(ref _isUserTypeMobile, value);
        }

        private bool _isUserTypeNonMobile;
        public bool IsUserTypeNonMobile
        {
            get => _isUserTypeNonMobile;
            set => SetProperty(ref _isUserTypeNonMobile, value);
        }

        private bool _isCheckedButtonA;
        public bool IsCheckedButtonA
        {
            get => _isCheckedButtonA;
            set 
            { 
                SetProperty(ref _isCheckedButtonA, value);
                PackAndShip();
            }
        }

        private bool _isCheckedButtonB;
        public bool IsCheckedButtonB
        {
            get => _isCheckedButtonB;
            set => SetProperty(ref _isCheckedButtonB, value);
        }

        private bool _isCheckedButtonC;
        public bool IsCheckedButtonC
        {
            get => _isCheckedButtonC;
            set => SetProperty(ref _isCheckedButtonC, value);
        }

        private bool _isCheckedButtonD;
        public bool IsCheckedButtonD
        {
            get => _isCheckedButtonD;
            set => SetProperty(ref _isCheckedButtonD, value);
        }

        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SubmitCommand { get; private set; }

        private void ExecuteCancelCommand()
        {
            this.RegionNavigationService.Journal.Clear();
            _eventAggregator.GetEvent<EditButtonsSubmitEvent>().Publish(null);
            SetUnsetProperties();
        }

        private async Task ExecuteSubmitCommand()
        {
            if (!this.IsCheckedButtonA && !this.IsCheckedButtonB && !this.IsCheckedButtonC && !this.IsCheckedButtonD)
            {
                MessageBox.Show("You must make a selection for Pack and Ship or Print or Mailboxes or Business Services or all.", "Required.");
                return;
            }
            else
            {
                var reqEntity = new UpdateButtonsRequestEntity
                {
                    Id = this.SelectedStoreUser.Id,
                    UserId = Convert.ToInt32(this.SelectedStoreUser.UserId),
                    SuperMasterId = Config.MasterStore.UserId,
                    Action = this.IsSelectedStoreUser ? "update_buttons" : "update_buttons_archive"
                };

                reqEntity.Button1 = string.Empty;
                reqEntity.Button2 = string.Empty;
                reqEntity.Button3 = string.Empty;
                reqEntity.Button4 = string.Empty;

                if (this.IsCheckedButtonA)
                {
                    reqEntity.Button1 = "Pack and Ship";
                }

                if (this.IsCheckedButtonB)
                {
                    reqEntity.Button2 = "Print";
                }

                if (this.IsCheckedButtonC)
                {
                    reqEntity.Button3 = "Mailboxes";
                }

                if (this.IsCheckedButtonD)
                {
                    reqEntity.Button4 = "Business Services";
                }

                var result = await _windowsManager.UpdateButtons(reqEntity);

                if (result.StatusCode == (int)GenericStatusValue.Success)
                {
                    if (Convert.ToBoolean(result.Status))
                    {
                        this.RegionNavigationService.Journal.Clear();
                        _eventAggregator.GetEvent<EditButtonsSubmitEvent>().Publish(new EditButtonsItemModel());
                        SetUnsetProperties();
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
            }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            if (navigationContext.Parameters[NavigationConstants.SelectedStoreUser] is StoreUserEntity selectedStoreUser)
                SelectedStoreUser = selectedStoreUser;

            if (navigationContext.Parameters[NavigationConstants.IsSelectedStoreUser] is bool isSelectedStoreUser)
                IsSelectedStoreUser = isSelectedStoreUser;

            this.IsCheckedButtonA = !string.IsNullOrWhiteSpace(SelectedStoreUser.Btn1);
            this.IsCheckedButtonB = !string.IsNullOrWhiteSpace(SelectedStoreUser.Btn2);
            this.IsCheckedButtonC = !string.IsNullOrWhiteSpace(SelectedStoreUser.Btn3);
            this.IsCheckedButtonD = !string.IsNullOrWhiteSpace(SelectedStoreUser.Btn4);
        }

        private void PackAndShip()
        {
            if (IsCheckedButtonA)
            {
                if (packAndShipPage != null)
                {
                    packAndShipPage.Close();
                    packAndShipPage = null;
                }

                packAndShipPage = new PackAndShipPage
                {
                    BaseHeight = ((System.Windows.Controls.Panel)Application.Current.MainWindow.Content).ActualHeight,
                    BaseWidth = ((System.Windows.Controls.Panel)Application.Current.MainWindow.Content).ActualWidth,
                    LocationManager = _locationManager
                };
                packAndShipPage.Show();
            }
            else
            {
                if (packAndShipPage != null)
                {
                    packAndShipPage.Close();
                    packAndShipPage = null;
                }
            }
        }

        private void SetUnsetProperties()
        {
            this.IsCheckedButtonA = this.IsCheckedButtonB = this.IsCheckedButtonC = this.IsCheckedButtonD = false;
            this.IsUserTypeMobile = false;
            this.IsUserTypeNonMobile = false;
            this.IsSelectedStoreUser = false;
        }
    }
}
