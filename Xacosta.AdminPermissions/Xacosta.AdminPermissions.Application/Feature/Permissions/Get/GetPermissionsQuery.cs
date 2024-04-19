using MediatR;

namespace Xacosta.AdminPermissions.Application.Feature
{
    public class GetPermissionsQuery : IRequest<IEnumerable<GetPermissionsResponse>>
    {
        public int Id { get; set; }
    }
}
