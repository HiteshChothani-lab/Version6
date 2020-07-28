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
        private readonly IWindowsManager _windowsManager;
        private readonly IEventAggregator _eventAggregator;

        public RegisterMasterStoreReviewPageViewModel(IRegionManager regionManager, 
            IWindowsManager windowsManager, IEventAggregator eventAggregator) : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _windowsManager = windowsManager;

            this.BackCommand = new DelegateCommand(() => ExecuteBackCommand());
            this.SubmitCommand = new DelegateCommand(async () => await ExecuteSubmitCommand());
        }


        private MasterStoreItemModel _masterStore;
        public MasterStoreItemModel MasterStore
        {
            get => _masterStore;
            set => SetProperty(ref _masterStore, value);
        }

        public DelegateCommand BackCommand { get; private set; }
        public DelegateCommand SubmitCommand { get; private set; }

        private void ExecuteBackCommand()
        {
            this.RegionNavigationService.Journal.CurrentEntry.Parameters.Add(NavigationConstants.MasterStoreModel, this.MasterStore);
            this.RegionNavigationService.Journal.GoBack();
        }


        private async Task ExecuteSubmitCommand()
        {
            try
            {
                var reqEntity = new RegisterMasterStoreRequestEntity()
                {
                    Address = this.MasterStore.Address,
                    AppVersionName = Config.CurrentUser.AppVersionName,
                    City = this.MasterStore.City.Name,
                    Country = this.MasterStore.Country.Name,
                    CountryCode = this.MasterStore.Country.PhoneCode,
                    DeviceId = getUniqueID("C"),
                    DeviceToken = "Push Notification Id",
                    DeviceType = "Windows",
                    PhoneNumber = this.MasterStore.PhoneNumber,
                    PostalCode = this.MasterStore.PostalCode,
                    State = this.MasterStore.State.Name,
                    StoreName = this.MasterStore.StoreName,
                    StorePrefferedLanguage = "English",
                    Street = this.MasterStore.Street,
                    UserId = this.MasterStore.UserId,
                    TimeZone = this.MasterStore.TimeZone
                };

                var result = await _windowsManager.RegisterMasterStore(reqEntity);
                PusherData.MasterStoreID = result.StoreId.ToString();
                await Pushers.Client.Init(_eventAggregator);

                if (result.StatusCode == (int)GenericStatusValue.Success)
                {
                    if (Convert.ToBoolean(result.Status))
                    {
                        var parameters = new NavigationParameters();
                        parameters.Add(NavigationConstants.RegisteredMasterStore, result);
                        this.RegionManager.RequestNavigate("ContentRegion", ViewNames.MainPage, parameters);
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
            catch (Exception ex)
            {
                string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembl‌​y().Location);
                path += "exception.txt";
                using (var outputFile = new StreamWriter(path, false, Encoding.UTF8))
                {
                    outputFile.WriteLine(ex.StackTrace);
                }
            }
        }

        private string getUniqueID(string drive)
        {
            if (drive == string.Empty)
            {
                //Find first drive
                foreach (var compDrive in DriveInfo.GetDrives())
                {
                    if (compDrive.IsReady)
                    {
                        drive = compDrive.RootDirectory.ToString();
                        break;
                    }
                }
            }

            if (drive.EndsWith(":\\"))
            {
                drive = drive.Substring(0, drive.Length - 2);
            }

            string volumeSerial = getVolumeSerial(drive);
            string cpuID = getCPUID();

            return cpuID.Substring(13) + cpuID.Substring(1, 4) + volumeSerial + cpuID.Substring(4, 4);
        }

        private string getVolumeSerial(string drive)
        {
            ManagementObject disk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
            disk.Get();

            string volumeSerial = disk["VolumeSerialNumber"].ToString();
            disk.Dispose();

            return volumeSerial;
        }

        private string getCPUID()
        {
            string cpuInfo = "";
            ManagementClass managClass = new ManagementClass("win32_processor");
            ManagementObjectCollection managCollec = managClass.GetInstances();

            foreach (var managObj in managCollec)
            {
                if (cpuInfo == "")
                {
                    cpuInfo = managObj.Properties["processorID"].Value.ToString();
                    break;
                }
            }

            return cpuInfo;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            if (navigationContext.Parameters.Any(x => x.Key == NavigationConstants.MasterStoreModel))
            {
                this.MasterStore = null;
                this.MasterStore = navigationContext.Parameters[NavigationConstants.MasterStoreModel] as MasterStoreItemModel;
            }
        }
    }
}
