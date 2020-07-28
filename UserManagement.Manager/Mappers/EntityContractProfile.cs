using AutoMapper;
using UserManagement.Entity;
using UserManagement.WebServices.DataContracts.Request;
using UserManagement.WebServices.DataContracts.Response;

namespace UserManagement.Manager.Mappers
{
    public class EntityContractProfile : Profile
    {
        public EntityContractProfile()
        {
            CreateMap<ValidateUserRequestEntity, ValidateUserRequestContract>();
            CreateMap<ValidateUserRequestContract, ValidateUserRequestEntity>();

            CreateMap<ValidateUserResponseEntity, ValidateUserResponseContract>();
            CreateMap<ValidateUserResponseContract, ValidateUserResponseEntity>();

			CreateMap<RegisterMasterStoreRequestEntity, RegisterMasterStoreRequestContract>();
			CreateMap<RegisterMasterStoreRequestContract, RegisterMasterStoreRequestEntity>();

			CreateMap<RegisterMasterStoreResponseEntity, RegisterMasterStoreResponseContract>();
			CreateMap<RegisterMasterStoreResponseContract, RegisterMasterStoreResponseEntity>();

            CreateMap<CheckUserRequestEntity, CheckUserRequestContract>();
            CreateMap<CheckUserRequestContract, CheckUserRequestEntity>();

            CreateMap<DefaultResponseEntity, DefaultResponseContract>();
            CreateMap<DefaultResponseContract, DefaultResponseEntity>();

            CreateMap<SaveUserDataRequestEntity, SaveUserDataRequestContract>();
            CreateMap<SaveUserDataRequestContract, SaveUserDataRequestEntity>();

            CreateMap<StoreUsersResponseEntity, StoreUsersResponseContract>();
            CreateMap<StoreUsersResponseContract, StoreUsersResponseEntity>();

            CreateMap<ArchieveStoreUsersResponseEntity, ArchieveStoreUsersResponseContract>();
            CreateMap<ArchieveStoreUsersResponseContract, ArchieveStoreUsersResponseEntity>();

            CreateMap<StoreUserEntity, StoreUserContract>();
            CreateMap<StoreUserContract, StoreUserEntity>();

            CreateMap<GetStoreUsersRequestEntity, GetStoreUsersRequestContract>();
            CreateMap<GetStoreUsersRequestContract, GetStoreUsersRequestEntity>();

            CreateMap<ManageUserRequestEntity, ManageUserRequestContract>();
            CreateMap<ManageUserRequestContract, ManageUserRequestEntity>();

            CreateMap<DeleteArchiveUserRequestEntity, DeleteArchiveUserRequestContract>();
            CreateMap<DeleteArchiveUserRequestContract, DeleteArchiveUserRequestEntity>();

            CreateMap<EditStoreUserRequestEntity, EditStoreUserRequestContract>();
            CreateMap<UpdateNonMobileStoreUserRequestEntity, UpdateNonMobileStoreUserRequestContract>();
            CreateMap<MoveStoreUserRequestEntity, MoveStoreUserRequestContract>();
            CreateMap<UpdateButtonsRequestEntity, UpdateButtonsRequestContract>();
            CreateMap<SetUnsetFlagRequestEntity, SetUnsetFlagRequestContract>();

        }
    }
}
