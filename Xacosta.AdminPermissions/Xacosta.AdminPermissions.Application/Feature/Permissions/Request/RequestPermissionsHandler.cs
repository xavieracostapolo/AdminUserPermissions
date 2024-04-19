using AutoMapper;
using MediatR;
using Xacosta.AdminPermissions.Application.Services;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;
using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Application.Feature
{
    public class RequestPermissionsHandler(
        IMapper _mapper, 
        IUnitOfWork _unitOfWork,
        IPublisherBrokerService _publisher) : IRequestHandler<RequestPermissionsCommand, RequestPermissionsResponse>
    {
        public async Task<RequestPermissionsResponse> Handle(RequestPermissionsCommand request, CancellationToken cancellationToken)
        {
            var master = _mapper.Map<PermissionType>(request);

            await _unitOfWork.Repository<PermissionType>().Insert(master);

            var detail = _mapper.Map<Permission>(request);
            detail.PermissionType = master;
            detail.FechaPermiso = DateOnly.FromDateTime(DateTime.Now);

            await _unitOfWork.Repository<Permission>().Insert(detail);

            await _unitOfWork.Commit();

            _publisher.Publish("request", detail, cancellationToken);

            return new RequestPermissionsResponse
            {
                Id = master.Id
            };
        }
    }
}
