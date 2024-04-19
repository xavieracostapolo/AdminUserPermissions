using AutoMapper;
using Moq;
using Xacosta.AdminPermissions.Application.Feature;
using Xacosta.AdminPermissions.Application.Services;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;
using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Tests.Application.Features.Permissions
{
    public class Request_Should
    {
        private readonly IMapper mapper;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<IPublisherBrokerService> mockPublisher;

        public Request_Should()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockPublisher = new Mock<IPublisherBrokerService>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<RequestPermissionsProfile>();
            });

            mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task RequestPermissions_Return_Id()
        {
            //Arrange
            Mock<IRepository<Permission>> mockIRepositoryPermission = new Mock<IRepository<Permission>>();
            Mock<IRepository<PermissionType>> mockIRepositoryPermissionType = new Mock<IRepository<PermissionType>>();
                        
            mockUnitOfWork.Setup(x => x.Repository<Permission>()).Returns(mockIRepositoryPermission.Object);
            mockUnitOfWork.Setup(x => x.Repository<PermissionType>()).Returns(mockIRepositoryPermissionType.Object);

            var handler = new RequestPermissionsHandler(mapper, mockUnitOfWork.Object, mockPublisher.Object);

            //Action
            var result = await handler.Handle(new RequestPermissionsCommand()
            {
                TipoPermiso = "TipoPermiso",
                NombreEmpleado = "NombreEmpleado",
                ApellidoEmpleado = "ApellidoEmpleado"
            }, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Id);
        }
    }
}
