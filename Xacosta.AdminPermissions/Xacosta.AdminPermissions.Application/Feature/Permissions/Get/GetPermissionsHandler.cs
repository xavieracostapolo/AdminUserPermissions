﻿using AutoMapper;
using MediatR;
using Xacosta.AdminPermissions.Application.Exceptions;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;
using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Application.Feature.Permissions.Get
{
    public class GetPermissionsHandler(IMapper _mapper, IUnitOfWork _unitOfWork) : IRequestHandler<GetPermissionsQuery, IEnumerable<GetPermissionsResponse>>
    {
        public async Task<IEnumerable<GetPermissionsResponse>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            var master = new PermissionType()
            {
                Descripcion = "Desc Type"
            };

            await _unitOfWork.Repository<PermissionType>().Insert(master);

            var detail = new Permission()
            {
                ApellidoEmpleado = "ApellidoEmpleado",
                FechaPermiso = DateOnly.FromDateTime(DateTime.Now),
                NombreEmpleado = "NombreEmpleado",
                PermissionType = master
            };

            await _unitOfWork.Repository<Permission>().Insert(detail);

            await _unitOfWork.Commit();

            var resp = await _unitOfWork.Repository<Permission>().Get();
            var resp2 = await _unitOfWork.Repository<PermissionType>().Get();


            if (resp == null)
                throw new NotFoundException();

            return _mapper.Map<IEnumerable<GetPermissionsResponse>>(resp);
        }
    }
}
