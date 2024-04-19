using AutoMapper;
using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Application.Feature
{
    public class RequestPermissionsProfile : Profile
    {
        public RequestPermissionsProfile()
        {
            CreateMap<Permission, RequestPermissionsCommand>().ReverseMap();
            CreateMap<RequestPermissionsCommand, PermissionType>()
                .ForMember(
                    dest => dest.Descripcion,
                    opt => opt.MapFrom(src => src.TipoPermiso)
                );
        }
    }
}
