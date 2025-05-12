using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Storied.Application.Features.Person.Commands;
using Storied.Application.Features.Person.Handlers;
using Storied.Application.Models;
using Storied.Application.Repositories;
using Storied.Domain.Common.Enums;
using Storied.Domain.Entities;

namespace Storied.Test
{
    /// <summary>  
    /// Unit tests for the <see cref="RecordBirthCommandHandler"/> class.  
    /// </summary>  
    public class RecordBirthCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IPersonRepository> _personRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly RecordBirthCommandHandler _handler;

        /// </summary>  
        public RecordBirthCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _personRepositoryMock = new Mock<IPersonRepository>();
            _mapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<RecordBirthCommandHandler>>();
            _handler = new RecordBirthCommandHandler(_unitOfWorkMock.Object, _personRepositoryMock.Object, _mapperMock.Object, loggerMock.Object);
        }

        /// <summary>  
        /// Tests that the <see cref="RecordBirthCommandHandler.Handle"/> method returns null when the person is not found.  
        /// </summary>  
        /// <returns>A task that represents the asynchronous operation.</returns>  
        [Fact]
        public async Task Handle_ShouldReturnNull_WhenPersonNotFound()
        {
            // Arrange  
            var command = new RecordBirthCommand { Id = Guid.NewGuid(), BirthDate = "2000-01-01", BirthLocation = "Location" };
            _personRepositoryMock.Setup(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>())).ReturnsAsync((Person)null!);

            // Act  
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert  
            Assert.Null(result);
        }

        /// <summary>  
        /// Tests that the <see cref="RecordBirthCommandHandler.Handle"/> method updates the person when the person exists.  
        /// </summary>  
        /// <returns>A task that represents the asynchronous operation.</returns>  
        [Fact]
        public async Task Handle_ShouldUpdatePerson_WhenPersonExists()
        {
            // Arrange  
            var command = new RecordBirthCommand { Id = Guid.NewGuid(), BirthDate = "2000-01-01", BirthLocation = "Location" };
            var person = new Person { Id = command.Id, Gender = Gender.Male, GivenName = "John", SurName = "Doe" };

            _personRepositoryMock.Setup(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>())).ReturnsAsync(person);
            _mapperMock.Setup(mapper => mapper.Map<RecordBirthCommandResponse>(person)).Returns(new RecordBirthCommandResponse { Id = person.Id });

            // Act  
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(command.Id, result.Id);
            _personRepositoryMock.Verify(repo => repo.Update(person), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Save(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
