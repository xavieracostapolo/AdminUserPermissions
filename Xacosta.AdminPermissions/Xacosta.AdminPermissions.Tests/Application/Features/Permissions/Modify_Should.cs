using Moq;
using System.Linq.Expressions;
using Xacosta.AdminPermissions.Application.Exceptions;
using Xacosta.AdminPermissions.Application.Feature;
using Xacosta.AdminPermissions.Application.Services;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;
using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Tests.Application.Features.Permissions
{
    public class Modify_Should
    {
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<IPublisherBrokerService> mockPublisher;

        public Modify_Should()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockPublisher = new Mock<IPublisherBrokerService>();
        }

        [Fact]
        public async Task ModifyPermissions_Return_NotFoundExceptionPermission()
        {
            //Arrange
            
            Mock<IRepository<Permission>> mockIRepositoryPermission = new Mock<IRepository<Permission>>();
            
            mockUnitOfWork.Setup(x => x.Repository<Permission>()).Returns(mockIRepositoryPermission.Object);

            var handler = new ModifyPermissionsHandler(mockUnitOfWork.Object, mockPublisher.Object);

            //Action
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(new ModifyPermissionsCommand(), CancellationToken.None));

            //Assert
        }

        [Fact]
        public async Task ModifyPermissions_Return_NotFoundExceptionPermissionType()
        {
            //Arrange
            Mock<IRepository<Permission>> mockIRepositoryPermission = new Mock<IRepository<Permission>>();
            Mock<IRepository<PermissionType>> mockIRepositoryPermissionType = new Mock<IRepository<PermissionType>>();

            mockIRepositoryPermission.Setup(x => x.GetByID(
                It.IsAny<object>()))
                .ReturnsAsync(new Permission());

            mockUnitOfWork.Setup(x => x.Repository<Permission>()).Returns(mockIRepositoryPermission.Object);
            mockUnitOfWork.Setup(x => x.Repository<PermissionType>()).Returns(mockIRepositoryPermissionType.Object);

            var handler = new ModifyPermissionsHandler(mockUnitOfWork.Object, mockPublisher.Object);

            //Action
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(new ModifyPermissionsCommand(), CancellationToken.None));

            //Assert
        }

        [Fact]
        public async Task ModifyPermissions_Return_Id()
        {
            //Arrange
            Mock<IRepository<Permission>> mockIRepositoryPermission = new Mock<IRepository<Permission>>();
            Mock<IRepository<PermissionType>> mockIRepositoryPermissionType = new Mock<IRepository<PermissionType>>();

            mockIRepositoryPermission.Setup(x => x.GetByID(
                It.IsAny<object>()))
                .ReturnsAsync(new Permission());

            mockIRepositoryPermissionType.Setup(x => x.GetByID(
               It.IsAny<object>()))
               .ReturnsAsync(new PermissionType());

            mockUnitOfWork.Setup(x => x.Repository<Permission>()).Returns(mockIRepositoryPermission.Object);
            mockUnitOfWork.Setup(x => x.Repository<PermissionType>()).Returns(mockIRepositoryPermissionType.Object);

            var handler = new ModifyPermissionsHandler(mockUnitOfWork.Object, mockPublisher.Object);

            //Action
            var result = await handler.Handle(new ModifyPermissionsCommand()
            {
                ApellidoEmpleado = "",
                NombreEmpleado = "",
                TipoPermiso = 1
            }, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Id);
        }
    }
}
