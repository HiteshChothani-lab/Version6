using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using UserManagement.Common.Constants;
using UserManagement.Entity;
using UserManagement.Manager;
using UserManagement.UI.Converters;

namespace UserManagement.UI.ViewModels
{
    public class Version4Buttons : BaseMainPage
    {
        private bool _isCheckedAppButton1;
        private bool _isCheckedAppButton2;
        private bool _isCheckedAppButton3;
        private bool _isCheckedAppButton3a;
        private bool _isCheckedAppButton3b;
        private bool _isCheckedAppButton3c;
        private Visibility actButton1Visibility = Visibility.Visible;
        private Visibility actButton2Visibility = Visibility.Visible;
        private Visibility actButton3Visibility = Visibility.Visible;
        private Visibility actButton4Visibility;
        private Visibility actButton5Visibility;
        private Visibility appButton1Visibility;
        private Visibility appButton2Visibility;
        private Visibility appButton3Visibility;
        private Visibility appButton3aVisibility;
        private Visibility appButton3bVisibility;
        private Visibility appButton3cVisibility;
        private Visibility appointmentButtonsVisibility;
        private Visibility appointmentGridVisibility;
        private Visibility firstNameVisibility;
        private Visibility lastNameVisibility;
        private Visibility logoutVisibility;
        private Visibility lowerBandVisibility;
        private Visibility middleBandVisibility;
        private Visibility mobileNumberVisibility;
        private Visibility nonMobileButtonVisibility;
        private Visibility lowerButtonsGridVisibility;
        private bool DoingChecked;

        public Version4Buttons(
            IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IWindowsManager windowsManager) :
            base(regionManager, eventAggregator, windowsManager)
        {
            AppointmentCheckedCommand = new DelegateCommand<string>(ExecuteAppointmentCheckedCommand);
            AppointmentUnCheckedCommand = new DelegateCommand<string>(ExecuteAppointmentUnCheckedCommand);

            if (Config.MasterStore.FacilityType == null)
                Config.MasterStore.FacilityType = "Clinic";

            var ThisClinic = Config.CurClinic;

            Debug.Assert(ThisClinic != null);

            ThisClinic.AppButtons.ForEach(SetElement);
            ThisClinic.ActButtons.ForEach(SetElement);
        }

        public Visibility ActButton1Visibility { get => actButton1Visibility; set => SetProperty(ref actButton1Visibility, value); }
        public Visibility ActButton2Visibility { get => actButton2Visibility; set => SetProperty(ref actButton2Visibility, value); }
        public Visibility ActButton3Visibility { get => actButton3Visibility; set => SetProperty(ref actButton3Visibility, value); }
        public Visibility ActButton4Visibility { get => actButton4Visibility; set => SetProperty(ref actButton4Visibility, value); }
        public Visibility ActButton5Visibility { get => actButton5Visibility; set => SetProperty(ref actButton5Visibility, value); }

        public Visibility AppButton1Visibility { get => appButton1Visibility; set => SetProperty(ref appButton1Visibility, value); }
        public Visibility AppButton2Visibility { get => appButton2Visibility; set => SetProperty(ref appButton2Visibility, value); }
        public Visibility AppButton3Visibility { get => appButton3Visibility; set => SetProperty(ref appButton3Visibility, value); }
        public Visibility AppButton3aVisibility { get => appButton3aVisibility; set => SetProperty(ref appButton3aVisibility, value); }
        public Visibility AppButton3bVisibility { get => appButton3bVisibility; set => SetProperty(ref appButton3bVisibility, value); }
        public Visibility AppButton3cVisibility { get => appButton3cVisibility; set => SetProperty(ref appButton3cVisibility, value); }

        public Visibility LowerButtonsGridVisibility { get => lowerButtonsGridVisibility; set => SetProperty(ref lowerButtonsGridVisibility, value); }

        public Visibility AppointmentGridVisibility { get => appointmentGridVisibility; set => SetProperty(ref appointmentGridVisibility, value); }
        public Visibility AppointmentButtonsVisibility { get => appointmentButtonsVisibility; set => SetProperty(ref appointmentButtonsVisibility, value); }
        public Visibility FirstNameVisibility { get => firstNameVisibility; set => SetProperty(ref firstNameVisibility, value); }
        public Visibility LastNameVisibility { get => lastNameVisibility; set => SetProperty(ref lastNameVisibility, value); }
        public Visibility LogoutVisibility { get => logoutVisibility; set => SetProperty(ref logoutVisibility, value); }
        public Visibility LowerBandVisibility { get => lowerBandVisibility; set => SetProperty(ref lowerBandVisibility, value); }
        public Visibility MiddleBandVisibility { get => middleBandVisibility; set => SetProperty(ref middleBandVisibility, value); }
        public Visibility MobileNumberVisibility { get => mobileNumberVisibility; set => SetProperty(ref mobileNumberVisibility, value); }
        public Visibility NonMobileButtonVisibility { get => nonMobileButtonVisibility; set => SetProperty(ref nonMobileButtonVisibility, value); }

        public string ActButton1CMD { get; set; }
        public string ActButton1Text { get; set; }
        public string ActButton2CMD { get; set; }
        public string ActButton2Text { get; set; }
        public string ActButton3CMD { get; set; }
        public string ActButton3Text { get; set; }
        public string ActButton4CMD { get; set; }
        public string ActButton4Text { get; set; }
        public string ActButton5CMD { get; set; }
        public string ActButton5Text { get; set; }

        public string AppButton1CMD { get; set; }
        public string AppButton1Icon1 { get; set; }
        public string AppButton1Icon2 { get; set; }
        public string AppButton1Text { get; set; }

        public string AppButton2CMD { get; set; }
        public string AppButton2Icon1 { get; set; }
        public string AppButton2Icon2 { get; set; }
        public string AppButton2Text { get; set; }

        public string AppButton3aCMD { get; set; }
        public string AppButton3aIcon1 { get; set; }
        public string AppButton3aIcon2 { get; set; }
        public string AppButton3aText { get; set; }

        public string AppButton3bCMD { get; set; }
        public string AppButton3bIcon1 { get; set; }
        public string AppButton3bIcon2 { get; set; }
        public string AppButton3bText { get; set; }

        public string AppButton3cCMD { get; set; }
        public string AppButton3cIcon1 { get; set; }
        public string AppButton3cIcon2 { get; set; }

        public string AppButton3CMD { get; set; }
        public string AppButton3cText { get; set; }
        public string AppButton3Icon1 { get; set; }
        public string AppButton3Icon2 { get; set; }
        public string AppButton3Text { get; set; }
        public DelegateCommand<string> AppointmentCheckedCommand { get; private set; }
        public DelegateCommand<string> AppointmentUnCheckedCommand { get; private set; }

        public bool IsCheckedAppButton1 { get => _isCheckedAppButton1; set => SetProperty(ref _isCheckedAppButton1, value); }
        public bool IsCheckedAppButton2 { get => _isCheckedAppButton2; set => SetProperty(ref _isCheckedAppButton2, value); }
        public bool IsCheckedAppButton3 { get => _isCheckedAppButton3; set => SetProperty(ref _isCheckedAppButton3, value); }
        public bool IsCheckedAppButton3a { get => _isCheckedAppButton3a; set => SetProperty(ref _isCheckedAppButton3a, value); }
        public bool IsCheckedAppButton3b { get => _isCheckedAppButton3b; set => SetProperty(ref _isCheckedAppButton3b, value); }
        public bool IsCheckedAppButton3c { get => _isCheckedAppButton3c; set => SetProperty(ref _isCheckedAppButton3c, value); }

        public bool CheckButtonsExpress()
        {
            if (IsCheckedAppButton1 || IsCheckedAppButton2 || IsCheckedAppButton3) return true;
            MessageBox.Show("You must make a selection for Appointment Type.", "Required."); return false;
        }

        public void SetAppButtons(SaveUserDataRequestEntity reqEntity)
        {
            reqEntity.Button1 = IsCheckedAppButton1 ? AppButton1CMD : string.Empty;
            reqEntity.Button2 = IsCheckedAppButton2 ? AppButton2CMD : string.Empty;
            reqEntity.Button3 = IsCheckedAppButton3 ? AppButton3CMD : string.Empty;
            reqEntity.Button3a = IsCheckedAppButton3a ? AppButton3aCMD : string.Empty;
            reqEntity.Button3b = IsCheckedAppButton3b ? AppButton3bCMD : string.Empty;
            reqEntity.Button3c = IsCheckedAppButton3c ? AppButton3cCMD : string.Empty;
        }

        public bool CheckSubmit()
        {
            if (IsCheckedAppButton1 || IsCheckedAppButton2 || IsCheckedAppButton3) return false;
            MessageBox.Show("You must make a selection for appointment type.", "Required.");
            return false;
        }

        public void SetServiceButtons()
        {
            IsCheckedAppButton1 = false;
            IsCheckedAppButton2 = false;
            IsCheckedAppButton3 = false;
            IsCheckedAppButton3a = false;
            IsCheckedAppButton3b = false;
            IsCheckedAppButton3c = false;
        }

        public void SetServiceButtons(StoreUserEntity selectedStoreUser)
        {
            IsCheckedAppButton1 = selectedStoreUser.Btn1.Equals(AppButton1Text);
            IsCheckedAppButton2 = selectedStoreUser.Btn2.Equals(AppButton2Text);
            IsCheckedAppButton3 = selectedStoreUser.Btn2.Equals(AppButton3Text);
        }

        public void UpdateServiceButtons(UpdateButtonsRequestEntity reqEntity)
        {
            reqEntity.Button1 = IsCheckedAppButton1 ? AppButton1Text : string.Empty;
            reqEntity.Button2 = IsCheckedAppButton2 ? AppButton2Text : string.Empty;
            reqEntity.Button3 = IsCheckedAppButton3 ? AppButton3Text : string.Empty;
        }

        protected new void ResetFields()
        {
            base.ResetFields();
            SetServiceButtons();
        }

        protected new void SetDelegateCommands()
        {
            base.SetDelegateCommands();
        }

        private void ExecuteAppointmentCheckedCommand(string x)
        {
            if (DoingChecked) return;
            DoingChecked = true;

            var Button1 = x == AppButton1CMD;
            var Button2 = x == AppButton2CMD;
            var Button3 = x == AppButton3CMD;
            var Button3a = x == AppButton3aCMD;
            var Button3b = x == AppButton3bCMD;
            var Button3c = x == AppButton3cCMD;

            if (Button1)
            {
                IsCheckedAppButton1 = true;
                IsCheckedAppButton2 = false;
            }
            else if (Button2)
            {
                IsCheckedAppButton1 = false;
                IsCheckedAppButton2 = true;
            }

            if (Button3)
            {
                IsCheckedAppButton3 = true;
                ShowExtraAppButtons();
            }

            IsCheckedAppButton3a = Button3a;
            IsCheckedAppButton3b = Button3b;
            IsCheckedAppButton3c = Button3c;
            DoingChecked = false;
        }

        private void ExecuteAppointmentUnCheckedCommand(string x)
        {
            if (DoingChecked) return;
            DoingChecked = true;
            logger.Info($"X is {x}");

            var Button1 = x == AppButton1CMD;
            var Button2 = x == AppButton2CMD;
            var Button3 = x == AppButton3CMD;
            var Button3a = x == AppButton3aCMD;
            var Button3b = x == AppButton3bCMD;
            var Button3c = x == AppButton3cCMD;

            if (Button1 && IsCheckedAppButton1)
                IsCheckedAppButton1 = false;

            if (Button2 && IsCheckedAppButton2)
                IsCheckedAppButton2 = false;

            if (!IsCheckedAppButton3 && Button3)
            {
                IsCheckedAppButton3 = false;
                IsCheckedAppButton3a = false;
                IsCheckedAppButton3b = false;
                IsCheckedAppButton3c = false;
                HideExtraAppButtons();
            }
            if (Button3a && IsCheckedAppButton3a)
                IsCheckedAppButton3a = false;
            if (Button3b && IsCheckedAppButton3b)
                IsCheckedAppButton3b = false;
            if (Button3c && IsCheckedAppButton3c)
                IsCheckedAppButton3c = false;

            DoingChecked = false;
        }

        private void HideExtraAppButtons()
        {
            logger.Info("hide");
            LowerButtonsGridVisibility = Visibility.Collapsed;
            AppButton3aVisibility = Visibility.Hidden;
            AppButton3bVisibility = Visibility.Hidden;
            AppButton3cVisibility = Visibility.Hidden;
            IsCheckedAppButton3 = false;
            IsCheckedAppButton3 = false;
            IsCheckedAppButton3 = false;
        }

        private void SetActButton1(IActionButton AB)
        {
            ActButton1Text = AB.ElementText;
            ActButton1CMD = AB.ElementCMD;
            switch (AB.Visible)
            {
                case "Visible": ActButton1Visibility = Visibility.Visible; break;
                case "Hidden": ActButton1Visibility = Visibility.Hidden; break;
                case "Collapsed": ActButton1Visibility = Visibility.Collapsed; break;
                default: Debug.Assert(false); break;
            };
        }

        private void SetActButton2(IActionButton AB)
        {
            ActButton2Text = AB.ElementText;
            ActButton2CMD = AB.ElementCMD;
            switch (AB.Visible)
            {
                case "Visible": ActButton2Visibility = Visibility.Visible; break;
                case "Hidden": ActButton2Visibility = Visibility.Hidden; break;
                case "Collapsed": ActButton2Visibility = Visibility.Collapsed; break;
                default: Debug.Assert(false); break;
            };
        }

        private void SetActButton3(IActionButton AB)
        {
            ActButton3Text = AB.ElementText;
            ActButton3CMD = AB.ElementCMD;
            switch (AB.Visible)
            {
                case "Visible": ActButton3Visibility = Visibility.Visible; break;
                case "Hidden": ActButton3Visibility = Visibility.Hidden; break;
                case "Collapsed": ActButton3Visibility = Visibility.Collapsed; break;
                default: Debug.Assert(false); break;
            };
        }

        private void SetButton1(IAppointmentButton AB)
        {
            AppButton1Text = AB.ElementText;
            AppButton1CMD = AB.ElementCMD;
            if (AB.Icon1 != "Nome") AppButton1Icon1 = $"/UserManagement.UI;component/Assets/{AB.Icon1}.png";
            if (AB.Icon2 != "Nome") AppButton1Icon2 = $"/UserManagement.UI;component/Assets/{AB.Icon2}.png";
            switch (AB.Visible)
            {
                case "Visible": AppButton1Visibility = Visibility.Visible; break;
                case "Hidden": AppButton1Visibility = Visibility.Hidden; break;
                case "Collapsed": AppButton1Visibility = Visibility.Collapsed; break;
                default: Debug.Assert(false); break;
            };
        }

        private void SetButton2(IAppointmentButton AB)
        {
            AppButton2Text = AB.ElementText;
            AppButton2CMD = AB.ElementCMD;
            if (AB.Icon1 != "Nome") AppButton2Icon1 = $"/UserManagement.UI; component/Assets/{AB.Icon1}.png";
            if (AB.Icon2 != "Nome") AppButton2Icon2 = $"/UserManagement.UI; component/Assets/{AB.Icon2}.png";
            switch (AB.Visible)
            {
                case "Visible": AppButton2Visibility = Visibility.Visible; break;
                case "Hidden": AppButton2Visibility = Visibility.Hidden; break;
                case "Collapsed": AppButton2Visibility = Visibility.Collapsed; break;
                default: Debug.Assert(false); break;
            };
        }

        private void SetButton3(IAppointmentButton AB)
        {
            AppButton3Text = AB.ElementText;
            AppButton3CMD = AB.ElementCMD;
            if (AB.Icon1 != "Nome") AppButton3Icon1 = $"/UserManagement.UI; component/Assets/{AB.Icon1}.png";
            if (AB.Icon2 != "Nome") AppButton3Icon2 = $"/UserManagement.UI; component/Assets/{AB.Icon2}.png";

            switch (AB.Visible)
            {
                case "Visible": AppButton3Visibility = Visibility.Visible; break;
                case "Hidden": AppButton3Visibility = Visibility.Hidden; break;
                case "Collapsed": AppButton3Visibility = Visibility.Collapsed; break;
                default: Debug.Assert(false); break;
            };
        }
        private void SetActButton5(IActionButton AB)
        {
            ActButton5Text = AB.ElementText;
            ActButton5CMD = AB.ElementCMD;

            switch (AB.Visible)
            {
                case "Visible": ActButton5Visibility = Visibility.Visible; break;
                case "Hidden": ActButton5Visibility = Visibility.Hidden; break;
                case "Collapsed": ActButton5Visibility = Visibility.Collapsed; break;
                default: Debug.Assert(false); break;
            };
        }
        private void SetActButton4(IActionButton AB)
        {
            ActButton4Text = AB.ElementText;
            ActButton4CMD = AB.ElementCMD;

            switch (AB.Visible)
            {
                case "Visible": ActButton4Visibility = Visibility.Visible; break;
                case "Hidden": ActButton4Visibility = Visibility.Hidden; break;
                case "Collapsed": ActButton4Visibility = Visibility.Collapsed; break;
                default: Debug.Assert(false); break;
            };
        }

        private void SetButton3a(IAppointmentButton AB)
        {
            AppButton3aText = AB.ElementText;
            AppButton3aCMD = AB.ElementCMD;
            if (AB.Icon1 != "Nome") AppButton3aIcon1 = $"/UserManagement.UI; component/Assets/{AB.Icon1}.png";
            if (AB.Icon2 != "Nome") AppButton3aIcon2 = $"/UserManagement.UI; component/Assets/{AB.Icon2}.png";

            switch (AB.Visible)
            {
                case "Visible": AppButton3aVisibility = Visibility.Visible; break;
                case "Hidden": AppButton3aVisibility = Visibility.Hidden; break;
                case "Collapsed": AppButton3aVisibility = Visibility.Collapsed; break;
                default: Debug.Assert(false); break;
            };
        }

        private void SetButton3b(IAppointmentButton AB)
        {
            AppButton3bText = AB.ElementText;
            AppButton3bCMD = AB.ElementCMD;
            if (AB.Icon1 != "Nome") AppButton3bIcon1 = $"/UserManagement.UI; component/Assets/{AB.Icon1}.png";
            if (AB.Icon2 != "Nome") AppButton3bIcon2 = $"/UserManagement.UI; component/Assets/{AB.Icon2}.png";

            switch (AB.Visible)
            {
                case "Visible": AppButton3bVisibility = Visibility.Visible; break;
                case "Hidden": AppButton3bVisibility = Visibility.Hidden; break;
                case "Collapsed": AppButton3bVisibility = Visibility.Collapsed; break;
                default: Debug.Assert(false); break;
            };
        }

        private void SetButton3c(IAppointmentButton AB)
        {
            AppButton3cText = AB.ElementText;
            AppButton3cCMD = AB.ElementCMD;
            if (AB.Icon1 != "Nome") AppButton3cIcon1 = $"/UserManagement.UI; component/Assets/{AB.Icon1}.png";
            if (AB.Icon2 != "Nome") AppButton3cIcon2 = $"/UserManagement.UI; component/Assets/{AB.Icon2}.png";
            switch (AB.Visible)
            {
                case "Visible": AppButton3cVisibility = Visibility.Visible; break;
                case "Hidden": AppButton3cVisibility = Visibility.Hidden; break;
                case "Collapsed": AppButton3cVisibility = Visibility.Collapsed; break;
                default: Debug.Assert(false); break;
            };
        }

        private void SetElement(IUIButton AB)
        {
            if (AB is IAppointmentButton ap)
                switch (ap.ElementType)
                {
                    case "AppButton1": SetButton1(ap); break;
                    case "AppButton2": SetButton2(ap); break;
                    case "AppButton3": SetButton3(ap); break;
                    case "AppButton3a": SetButton3a(ap); break;
                    case "AppButton3b": SetButton3b(ap); break;
                    case "AppButton3c": SetButton3c(ap); break;
                    default: Debug.Assert(false, $"{ap.ElementType}"); break;
                }
            else if (AB is IActionButton act)
                switch (act.ElementType)
                {
                    case "ActButton1": SetActButton1(act); break;
                    case "ActButton2": SetActButton2(act); break;
                    case "ActButton3": SetActButton3(act); break;
                    case "ActButton4": SetActButton4(act); break;
                    case "ActButton5": SetActButton5(act); break;
                    default: Debug.Assert(false, $"{act.ElementType}"); break;
                }
            else Debug.Assert(false, $"{AB.ElementType}");

        }
        private void ShowExtraAppButtons()
        {
            logger.Info("show");

            LowerButtonsGridVisibility = Visibility.Visible;
            AppButton3aVisibility = Visibility.Visible;
            AppButton3bVisibility = Visibility.Visible;
            AppButton3cVisibility = Visibility.Visible;
        }
    }
}