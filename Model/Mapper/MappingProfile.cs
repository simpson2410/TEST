using AutoMapper;
using Entities.Entities;
using Model.APIs;
using Model.Permissions;

namespace Model.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Role
            CreateMap<RoleModel, ApplicationRole>().ReverseMap();
            CreateMap<RoleMenuPermissionModel, RoleMenuPermission>().ReverseMap();
            CreateMap<UserRoleViewModel, ApplicationUserRole>().ReverseMap();
            #endregion

            #region Danh muc
          
            CreateMap<MenuModel, NavigationMenu>().ReverseMap();
          
            CreateMap<Contact, ContactModel>().ReverseMap();
            CreateMap<StoreRegister, StoreRegisterRequest>().ReverseMap();
            #endregion

        }
    }
}
