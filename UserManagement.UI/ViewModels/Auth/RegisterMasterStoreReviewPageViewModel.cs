using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UserManagement.Common.Constants;
using UserManagement.Common.Enums;
using UserManagement.Entity;
using UserManagement.Manager;
using UserManagement.UI.ItemModels;

namespace UserManagement.UI.ViewModels
{
    public class RegisterMasterStoreReviewPageViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowsManager _windowsManager;
        private MasterStoreItemModel _masterStore;

        public RegisterMasterStoreReviewPageViewModel(IRegionManager regionManager,
                    IWindowsManager windowsManager, IEventAggregator eventAggregator) : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _windowsManager = windowsManager;

            BackCommand = new DelegateCommand(() => ExecuteBackCommand());
            SubmitCommand = new DelegateCommand(async () => await ExecuteSubmitCommand());
        }

        public DelegateCommand BackCommand { get; private set; }

        public MasterStoreItemModel MasterStore
        {
            get => _masterStore;
            set => SetProperty(ref _masterStore, value);
        }

        public DelegateCommand SubmitCommand { get; private set; }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            if (navigationContext.Parameters.All(x => x.Key != NavigationConstants.MasterStoreModel)) return;
            MasterStore = null;
            MasterStore = navigationContext.Parameters[NavigationConstants.MasterStoreModel] as MasterStoreItemModel;
        }

        private void ExecuteBackCommand()
        {
            RegionNavigationService.Journal.CurrentEntry.Parameters.Add(NavigationConstants.MasterStoreModel, MasterStore);
            RegionNavigationService.Journal.GoBack();
        }

        private async Task ExecuteSubmitCommand()
        {
           
                RegisterMasterStoreRequestEntity reqEntity;
                reqEntity = new RegisterMasterStoreRequestEntity();
                reqEntity.Address = MasterStore.Address;
                reqEntity.AppVersionName = Config.CurrentUser.AppVersionName;
                reqEntity.City = MasterStore.City.Name;
                reqEntity.Country = MasterStore.Country.Name;
                reqEntity.CountryCode = MasterStore.Country.PhoneCode;
                reqEntity.DeviceId = getUniqueID("C");
                reqEntity.DeviceToken = "Push Notification Id";
                reqEntity.DeviceType = "Windows";
                reqEntity.PhoneNumber = MasterStore.PhoneNumber;
                reqEntity.PostalCode = MasterStore.PostalCode;
                reqEntity.State = MasterStore.State.Name;
                reqEntity.StoreName = MasterStore.StoreName;
                reqEntity.FacilityType = MasterStore.FacilityType;
                reqEntity.StorePreferedLanguage = "English";
                reqEntity.Street = MasterStore.Street;
                reqEntity.UserId = MasterStore.UserId;
                reqEntity.TimeZone = MasterStore.TimeZone;
                reqEntity.SelectedTimeZone = MasterStore.SelectedTimeZone;

                var result = await _windowsManager.RegisterMasterStore(reqEntity);
                PusherData.MasterStoreID = result.StoreId.ToString();
                await Pushers.Client.Init(_eventAggregator);

                switch (result.StatusCode)
                {
                    case (int)GenericStatusValue.Success when Convert.ToBoolean(result.Status):
                        {
                            var parameters = new NavigationParameters();
                            parameters.Add(NavigationConstants.RegisteredMasterStore, result);
                            RegionManager.RequestNavigate("ContentRegion", ViewNames.MainPage, parameters);
                            break;
                        }
                    case (int)GenericStatusValue.Success:
                        MessageBox.Show(result.Message, "Unsuccessful");
                        break;

                    case (int)GenericStatusValue.NoInternetConnection:
                        MessageBox.Show(MessageBoxMessage.NoInternetConnection, "Unsuccessful");
                        break;

                    case (int)GenericStatusValue.HasErrorMessage:
                        MessageBox.Show(((EntityBase)result).Message, "Unsuccessful");
                        break;

                    default:
                        MessageBox.Show(MessageBoxMessage.UnknownErorr, "Unsuccessful");
                        break;
                }
            
            
        }

        private string getCPUID()
        {
            var cpuInfo = "";
            var managClass = new ManagementClass("win32_processor");
            var managCollec = managClass.GetInstances();

            foreach (var managObj in managCollec)
            {
                if (cpuInfo != "") continue;
                cpuInfo = managObj.Properties["processorID"].Value.ToString();
                break;
            }

            return cpuInfo;
        }

        private string getUniqueID(string drive)
        {
            if (drive == string.Empty)
                //Find first drive
                foreach (var compDrive in DriveInfo.GetDrives())
                    if (compDrive.IsReady)
                    {
                        drive = compDrive.RootDirectory.ToString();
                        break;
                    }

            if (drive.EndsWith(":\\"))
                drive = drive.Substring(0, drive.Length - 2);

            var volumeSerial = getVolumeSerial(drive);
            var cpuID = getCPUID();

            var substring = cpuID.Substring(13);
            return $"{substring} {cpuID.Substring(1, 4)} {volumeSerial} {cpuID.Substring(4, 4)}";
        }

        private string getVolumeSerial(string drive)
        {
            ManagementObject disk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
            disk.Get();

            string volumeSerial = disk["VolumeSerialNumber"].ToString();
            disk.Dispose();

            return volumeSerial;
        }
    }
}