using AutoMapper;
using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Application.Feature
{
    public class GetPermissionsProfile : Profile
    {
        public GetPermissionsProfile()
        {
            CreateMap<Permission, GetPermissionsResponse>().ReverseMap();
        }
    }
}
