using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Prism.Commands;
using Prism.Regions;
using UserManagement.Entity;
using UserManagement.UI.ItemModels;

namespace UserManagement.UI.ViewModels
{
    public interface IMainPageViewModel
    {
        ObservableCollection<StoreUserEntity> StoreUsers { get; set; }
        ObservableCollection<StoreUserEntity> ArchieveStoreUsers { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string MobileNumber { get; set; }
        NonMobileUserItemModel NonMobileUser { get; set; }
        bool IsCheckedVeryGood { get; set; }
        bool IsCheckedIndifferent { get; set; }
        bool IsCheckedVeryTerrible { get; set; }
        bool IsCheckedVeryTerribleNoneDeal { get; set; }
        bool IsCheckedVeryTerribleTerribleService { get; set; }
        bool IsCheckedVeryTerribleNoneDealTerribleService { get; set; }
        bool IsCheckedVeryTerribleNone { get; set; }
        bool CanTapAddCommand { get; set; }
        Visibility LoaderVisibility { get; set; }
        string LoaderMessage { get; set; }
        string ExpressTime { get; set; }
        DelegateCommand NonMobileUserCommand { get; }
        DelegateCommand AddUserCommand { get; }
        DelegateCommand ExpressUserCommand { get; }
        DelegateCommand<string> VeryTerribleCheckedCommand { get; }
        DelegateCommand<string> VeryTerribleUncheckedCommand { get; }
        DelegateCommand<StoreUserEntity> DeleteStoreUserCommand { get; }
        DelegateCommand<StoreUserEntity> DeleteArchiveUserCommand { get; }
        DelegateCommand<StoreUserEntity> EditStoreUserCommand { get; }
        DelegateCommand<StoreUserEntity> SetFlagCommand { get; }
        DelegateCommand<StoreUserEntity> EditNonMobileStoreUserCommand { get; }
        DelegateCommand<StoreUserEntity> EditAgeOrNeedleUserCommand { get; }
        DelegateCommand<StoreUserEntity> EditArchiveAgeOrNeedleUserCommand { get; }
        DelegateCommand<StoreUserEntity> EditNonMobileArchiveStoreUserCommand { get; }
        DelegateCommand<StoreUserEntity> MoveStoreUserCommand { get; }
        DelegateCommand LogoutCommand { get; }
        DelegateCommand RefreshDataCommand { get; }
        DelegateCommand<StoreUserEntity> StoreIDCheckedCommand { get; }
        DelegateCommand<StoreUserEntity> ArchiveIDCheckedCommand { get; }
        DelegateCommand<StoreUserEntity> UserDetailWindowCommand { get; }
        void OnNavigatedTo(NavigationContext navigationContext);
        bool IsNavigationTarget(NavigationContext navigationContext);
        void OnNavigatedFrom(NavigationContext navigationContext);
        event PropertyChangedEventHandler PropertyChanged;
    }
}