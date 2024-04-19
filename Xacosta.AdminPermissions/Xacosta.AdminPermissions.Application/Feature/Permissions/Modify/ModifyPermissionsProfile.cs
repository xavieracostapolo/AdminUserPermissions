using AutoMapper;
using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Application.Feature
{
    public class ModifyPermissionsProfile : Profile
    {
        public ModifyPermissionsProfile()
        {
            CreateMap<ModifyPermissionsCommand, Permission>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.IdPermiso)
                );

            CreateMap<ModifyPermissionsCommand, PermissionType>()
                .ForMember(
                    dest => dest.Descripcion,
                    opt => opt.MapFrom(src => src.TipoPermiso)
                );
        }
    }
}
