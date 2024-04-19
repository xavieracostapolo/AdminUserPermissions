using AutoMapper;
using Moq;
using System.Linq.Expressions;
using Xacosta.AdminPermissions.Application.Feature;
using Xacosta.AdminPermissions.Application.Services;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;
using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Tests.Application.Features.Permissions
{
    public class Get_Should
    {
        private readonly IMapper mapper;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<IPublisherBrokerService> mockPublisher;

        public Get_Should()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockPublisher = new Mock<IPublisherBrokerService>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<GetPermissionsProfile>();
            });

            mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetPermissions_Return_Items()
        {
            //Arrange
            Mock<IRepository<Permission>> mockIRepositoryPermission = new Mock<IRepository<Permission>>();
            
            IEnumerable<Permission> data = new List<Permission>()
            {
                new Permission()
            };

            mockIRepositoryPermission.Setup(x => x.Get(
                It.IsAny<Expression<Func<Permission, bool>>>(), 
                It.IsAny<Func<IQueryable<Permission>, IOrderedQueryable<Permission>>>(),
                It.IsAny<string>()))
                .ReturnsAsync(data);
            mockUnitOfWork.Setup(x => x.Repository<Permission>()).Returns(mockIRepositoryPermission.Object);
            
            var handler = new GetPermissionsHandler(mapper, mockUnitOfWork.Object, mockPublisher.Object);

            //Action
            var result = await handler.Handle(new GetPermissionsQuery(), CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Any());
        }
    }
}
