using AutoMapper;
using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Application.Feature.Permissions.Get
{
    internal class GetPermissionsProfile : Profile
    {
        public GetPermissionsProfile()
        {
            CreateMap<Permission, GetPermissionsResponse>();
        }
    }
}
