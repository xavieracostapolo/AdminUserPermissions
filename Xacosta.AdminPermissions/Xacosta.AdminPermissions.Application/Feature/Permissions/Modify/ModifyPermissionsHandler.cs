using MediatR;
using Xacosta.AdminPermissions.Application.Exceptions;
using Xacosta.AdminPermissions.Application.Services;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;
using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Application.Feature
{
    public class ModifyPermissionsHandler(
        IUnitOfWork _unitOfWork,
        IPublisherBrokerService _publisher) : IRequestHandler<ModifyPermissionsCommand, ModifyPermissionsResponse>
    {
        public async Task<ModifyPermissionsResponse> Handle(ModifyPermissionsCommand request, CancellationToken cancellationToken)
        {
            var permissioEntity = await _unitOfWork.Repository<Permission>().GetByID(request.IdPermiso);
            if (permissioEntity == null)
                throw new NotFoundException("Permissions not exist.");

            var permissionTypeEntity = await _unitOfWork.Repository<PermissionType>().GetByID(request.TipoPermiso);
            if (permissionTypeEntity == null)
                throw new NotFoundException("PermissionsType not exist.");

            permissioEntity.PermissionType = permissionTypeEntity;
            permissioEntity.FechaPermiso = DateOnly.FromDateTime(DateTime.Now);
            permissioEntity.NombreEmpleado = request.NombreEmpleado;
            permissioEntity.ApellidoEmpleado = request.ApellidoEmpleado;

            _unitOfWork.Repository<Permission>().Update(permissioEntity);

            await _unitOfWork.Commit();

            _publisher.Publish("modify", permissioEntity, cancellationToken);

            return new ModifyPermissionsResponse()
            {
                Id = permissioEntity.Id
            };
        }
    }
}
