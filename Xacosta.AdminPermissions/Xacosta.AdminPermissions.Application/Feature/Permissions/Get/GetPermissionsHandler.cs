using AutoMapper;
using MediatR;
using Xacosta.AdminPermissions.Application.Services;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;
using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Application.Feature
{
    public class GetPermissionsHandler(
        IMapper _mapper,
        IUnitOfWork _unitOfWork,
        IPublisherBrokerService _publisher)
        : IRequestHandler<GetPermissionsQuery, IEnumerable<GetPermissionsResponse>>
    {
        public async Task<IEnumerable<GetPermissionsResponse>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            var resp = await _unitOfWork.Repository<Permission>().Get();

            if (resp.Any())
                _publisher.Publish("get", resp.FirstOrDefault(), cancellationToken);

            return _mapper.Map<IEnumerable<GetPermissionsResponse>>(resp);
        }
    }
}
