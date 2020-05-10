using Prism.Commands;
using Prism.Regions;
using System;
using System.ComponentModel;
using System.Windows;
using UserManagement.Entity;

namespace UserManagement.UI.ViewModels
{
    public interface IButtons
    {
        event PropertyChangedEventHandler PropertyChanged;

        bool CheckButtonsExpress();

        void CheckServiceButtons(SaveUserDataRequestEntity reqEntity);

        bool CheckSubmit();
        bool IsNavigationTarget(NavigationContext navigationContext);
        void OnNavigatedFrom(NavigationContext navigationContext);
        void OnNavigatedTo(NavigationContext navigationContext);
        void SetButtonCommands();
        void SetServiceButtons(StoreUserEntity selectedStoreUser);

        void SetServiceButtons();

        void UpdateServiceButtons(UpdateButtonsRequestEntity reqEntity);
    }

    public interface IOldButtons  : IButtons
    {
        bool IsCheckedIndifferent { get; set; }
        bool IsCheckedVeryGood { get; set; }
        bool IsCheckedVeryTerrible { get; set; }
        bool IsCheckedVeryTerribleNone { get; set; }
        bool IsCheckedVeryTerribleNoneDeal { get; set; }
        bool IsCheckedVeryTerribleNoneDealTerribleService { get; set; }
        bool IsCheckedVeryTerribleTerribleService { get; set; }
        DelegateCommand<string> VeryTerribleCheckedCommand { get; }
        DelegateCommand<string> VeryTerribleUncheckedCommand { get; }
    }

    public class OldButtons : ViewModelBase
    {
        private bool _isCheckedIndifferent;
        private bool _isCheckedVeryGood;
        private bool _isCheckedVeryTerrible;
        private bool _isCheckedVeryTerribleNone;
        private bool _isCheckedVeryTerribleNoneDeal;
        private bool _isCheckedVeryTerribleNoneDealTerribleService;
        private bool _isCheckedVeryTerribleTerribleService;

        public OldButtons(IRegionManager regionManager) : base(regionManager)
        {
        }

        public bool IsCheckedIndifferent
        {
            get => _isCheckedIndifferent;
            set => SetProperty(ref _isCheckedIndifferent, value);
        }

        public bool IsCheckedVeryGood
        {
            get => _isCheckedVeryGood;
            set => SetProperty(ref _isCheckedVeryGood, value);
        }

        public bool IsCheckedVeryTerrible
        {
            get => _isCheckedVeryTerrible;
            set => SetProperty(ref _isCheckedVeryTerrible, value);
        }

        public bool IsCheckedVeryTerribleNone
        {
            get => _isCheckedVeryTerribleNone;
            set => SetProperty(ref _isCheckedVeryTerribleNone, value);
        }

        public bool IsCheckedVeryTerribleNoneDeal
        {
            get => _isCheckedVeryTerribleNoneDeal;
            set => SetProperty(ref _isCheckedVeryTerribleNoneDeal, value);
        }

        public bool IsCheckedVeryTerribleNoneDealTerribleService
        {
            get => _isCheckedVeryTerribleNoneDealTerribleService;
            set => SetProperty(ref _isCheckedVeryTerribleNoneDealTerribleService, value);
        }

        public bool IsCheckedVeryTerribleTerribleService
        {
            get => _isCheckedVeryTerribleTerribleService;
            set => SetProperty(ref _isCheckedVeryTerribleTerribleService, value);
        }

        public DelegateCommand<string> VeryTerribleCheckedCommand { get; private set; }

        public DelegateCommand<string> VeryTerribleUncheckedCommand { get; private set; }

        public bool CheckButtonsExpress()

        {
            if (IsCheckedVeryGood || IsCheckedIndifferent) return false;
            MessageBox.Show("You must make a selection for very Good or indifferent or both.", "Required.");
            return true;
        }

        public void CheckServiceButtons(SaveUserDataRequestEntity reqEntity)
        {

            reqEntity.Button1 = IsCheckedVeryGood ? "Very Good" : string.Empty;
            reqEntity.Button2 = IsCheckedIndifferent ? "Indifferent" : string.Empty;

            if (IsCheckedVeryTerribleNone) reqEntity.Button3 = "Very Terrible";
            else if (IsCheckedVeryTerribleNoneDeal) reqEntity.Button3 = "No Deals";
            else if (IsCheckedVeryTerribleTerribleService) reqEntity.Button3 = "Terrible Service";
            else if (IsCheckedVeryTerribleNoneDealTerribleService) reqEntity.Button3 = "No deals & Terrible Service";
            else reqEntity.Button3 = string.Empty;
        }

        public bool CheckSubmit()
        {
            if (IsCheckedVeryGood || IsCheckedIndifferent) return true;
            MessageBox.Show("You must make a selection for very Good or indifferent or both.", "Required.");
            return false;
        }
        public void SetButtonCommands()
        {
            VeryTerribleCheckedCommand = new DelegateCommand<string>(ExecuteVeryTerribleCheckedCommand);
            VeryTerribleUncheckedCommand = new DelegateCommand<string>(ExecuteVeryTerribleUncheckedCommand);
        }

        public void SetServiceButtons(StoreUserEntity selectedStoreUser)
        {
            IsCheckedVeryGood = selectedStoreUser.Btn1.Equals("Very Good");
            IsCheckedIndifferent = selectedStoreUser.Btn2.Equals("Indifferent");
            IsCheckedVeryTerrible = !string.IsNullOrWhiteSpace(selectedStoreUser.Btn3);

            if (selectedStoreUser.Btn3.Equals("Very Terrible")) IsCheckedVeryTerribleNone = true;
            else if (selectedStoreUser.Btn3.Equals("No Deals")) IsCheckedVeryTerribleNoneDeal = true;
            else if (selectedStoreUser.Btn3.Equals("Terrible Service")) IsCheckedVeryTerribleTerribleService = true;
            else if (selectedStoreUser.Btn3.Equals("No deals ")) IsCheckedVeryTerribleNoneDealTerribleService = true;
        }

        public void SetServiceButtons()
        {
            IsCheckedIndifferent = false;
            IsCheckedVeryGood = false;

            IsCheckedVeryTerribleNone = false;
            IsCheckedVeryTerrible = false;
            IsCheckedVeryTerribleNoneDeal = false;
            IsCheckedVeryTerribleTerribleService = false;
            IsCheckedVeryTerribleNoneDealTerribleService = false;
        }

        public void UpdateServiceButtons(UpdateButtonsRequestEntity reqEntity)
        {
            reqEntity.Button1 = IsCheckedVeryGood ? "Very Good" : string.Empty;
            reqEntity.Button2 = IsCheckedIndifferent ? "Indifferent" : string.Empty;

            if (IsCheckedVeryTerribleNone) reqEntity.Button3 = "Very Terrible";
            else if (IsCheckedVeryTerribleNoneDeal) reqEntity.Button3 = "No Deals";
            else if (IsCheckedVeryTerribleTerribleService) reqEntity.Button3 = "Terrible Service";
            else if (IsCheckedVeryTerribleNoneDealTerribleService) reqEntity.Button3 = "No deals & Terrible Service";
            else reqEntity.Button3 = string.Empty;
        }
        private void ExecuteVeryTerribleCheckedCommand(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                IsCheckedVeryTerribleNone = true;
                IsCheckedVeryTerribleNoneDeal = false;
                IsCheckedVeryTerribleTerribleService = false;
                IsCheckedVeryTerribleNoneDealTerribleService = false;
            }
            else switch (parameter)
            {
                case "1":
                    IsCheckedVeryTerribleNone = false;
                    IsCheckedVeryTerribleNoneDeal = true;
                    IsCheckedVeryTerribleTerribleService = false;
                    IsCheckedVeryTerribleNoneDealTerribleService = false;
                    break;

                case "2":
                    IsCheckedVeryTerribleNone = false;
                    IsCheckedVeryTerribleNoneDeal = false;
                    IsCheckedVeryTerribleTerribleService = true;
                    IsCheckedVeryTerribleNoneDealTerribleService = false;
                    break;

                case "3":
                    IsCheckedVeryTerribleNone = false;
                    IsCheckedVeryTerribleNoneDeal = false;
                    IsCheckedVeryTerribleTerribleService = false;
                    IsCheckedVeryTerribleNoneDealTerribleService = true;
                    break;

                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void ExecuteVeryTerribleUncheckedCommand(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                IsCheckedVeryTerribleNone = false;
                IsCheckedVeryTerribleNoneDeal = false;
                IsCheckedVeryTerribleTerribleService = false;
                IsCheckedVeryTerribleNoneDealTerribleService = false;
            }
            else switch (parameter)
            {
                case "1":
                    if (IsCheckedVeryTerribleNone)
                        IsCheckedVeryTerribleNone = IsCheckedVeryTerribleTerribleService == false &&
                                                    IsCheckedVeryTerribleNoneDealTerribleService == false;
                    break;
                case "2":
                    if (IsCheckedVeryTerribleNone)
                        IsCheckedVeryTerribleNone = IsCheckedVeryTerribleNoneDeal == false &&
                                                    IsCheckedVeryTerribleNoneDealTerribleService == false;
                    break;
                case "3":
                    if (IsCheckedVeryTerribleNone)
                        IsCheckedVeryTerribleNone = IsCheckedVeryTerribleNoneDeal == false &&
                                                    IsCheckedVeryTerribleTerribleService == false;
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}