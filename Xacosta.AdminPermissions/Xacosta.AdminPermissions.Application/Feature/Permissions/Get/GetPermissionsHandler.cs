using AutoMapper;
using MediatR;
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
            var resp = _unitOfWork.Repository<Permission>().GetById(request.Id);

            if (resp == null)
                throw new Exception();

            return _mapper.Map<GetPermissionsResponse>(resp);
        }
    }
}
