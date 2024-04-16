using MediatR;

namespace Xacosta.AdminPermissions.Application.Feature.Permissions.Get
{
    public class GetPermissionsQuery : IRequest<GetPermissionsResponse>
    {
        public int Id { get; set; }
    }
}
