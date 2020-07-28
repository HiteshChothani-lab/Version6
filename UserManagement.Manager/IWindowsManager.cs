using System.Threading.Tasks;
using UserManagement.Entity;

namespace UserManagement.Manager
{
	public interface IWindowsManager
	{
        void Logout();

        Task<ValidateUserResponseEntity> ValidateUser(ValidateUserRequestEntity reqEntity);
		Task<RegisterMasterStoreResponseEntity> RegisterMasterStore(RegisterMasterStoreRequestEntity reqEntity);
        Task<DefaultResponseEntity> CheckStoreUser(CheckUserRequestEntity reqEntity);
        Task<DefaultResponseEntity> SaveUserData(SaveUserDataRequestEntity reqEntity, bool dummy);
        Task<StoreUsersResponseEntity> GetStoreUsers(GetStoreUsersRequestEntity reqEntity);
        Task<ArchieveStoreUsersResponseEntity> GetArchieveStoreUsers(GetStoreUsersRequestEntity reqEntity);
        Task<DefaultResponseEntity> DeleteStoreUser(DeleteStoreUserRequestEntity reqEntity);
        Task<DefaultResponseEntity> ManageUser(ManageUserRequestEntity reqEntity);
        Task<DefaultResponseEntity> CheckIDRArchiveUser(ManageUserRequestEntity reqEntity);
        Task<DefaultResponseEntity> CheckIDRStoreUser(ManageUserRequestEntity reqEntity);
        Task<DefaultResponseEntity> DeleteArchiveUser(DeleteArchiveUserRequestEntity reqEntity);
        Task<DefaultResponseEntity> EditStoreUser(EditStoreUserRequestEntity reqEntity);
        Task<DefaultResponseEntity> UpdateNonMobileStoreUser(UpdateNonMobileStoreUserRequestEntity reqEntity);
        Task<DefaultResponseEntity> MoveStoreUser(MoveStoreUserRequestEntity reqEntity);
        Task<DefaultResponseEntity> UpdateButtons(UpdateButtonsRequestEntity reqEntity);
        Task<DefaultResponseEntity> SetUnsetFlag(SetUnsetFlagRequestEntity reqEntity);
    }
}