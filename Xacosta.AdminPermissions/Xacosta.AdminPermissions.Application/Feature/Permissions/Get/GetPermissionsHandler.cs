using AutoMapper;
using MediatR;
using Xacosta.AdminPermissions.Application.Exceptions;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;
using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Application.Feature.Permissions.Get
{
    public class GetPermissionsHandler : IRequestHandler<GetPermissionsQuery, GetPermissionsResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;        

        public GetPermissionsHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetPermissionsResponse> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            var resp = await _unitOfWork.Repository<Permission>().Get();

            if (resp == null)
                throw new NotFoundException();

            return _mapper.Map<GetPermissionsResponse>(resp);
        }
    }
}
