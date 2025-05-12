using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Storied.Application.Common.Enums;
using Storied.Application.Features.Person.Commands;
using Storied.Application.Features.Person.Handlers;
using Storied.Application.Models;
using Storied.Application.Repositories;
using Storied.Domain.Common.Enums;
using Storied.Domain.Entities;

namespace Storied.Test;

/// </summary>  
public class AddPersonCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly AddPersonCommandHandler _handler;

    /// <summary>  
    /// Initializes a new instance of the <see cref="AddPersonCommandHandlerTests"/> class.  
    /// Sets up mocks and the handler instance for testing.  
    /// </summary>  
    public AddPersonCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _personRepositoryMock = new Mock<IPersonRepository>();
        _mapperMock = new Mock<IMapper>();
        Mock<ILogger<AddPersonCommandHandler>> loggerMock = new();
        _handler = new AddPersonCommandHandler(_unitOfWorkMock.Object, _personRepositoryMock.Object, _mapperMock.Object, loggerMock.Object);
    }

   

    /// <summary>  
    /// Tests the <see cref="AddPersonCommandHandler.Handle"/> method to ensure it adds a person and returns the correct response.  
    /// </summary>  
    /// <returns>A task that represents the asynchronous test operation.</returns>  
    [Fact]
    public async Task Handle_ShouldAddPersonAndReturnResponse()
    {
        // Arrange  
        var command = new AddPersonCommand(
            GivenName: "John",
            SurName: "Doe",
            Gender: "Male",
            BirthDate: null,
            BirthLocation: null,
            DeathDate: null,
            DeathLocation: null
        );
        var person = new Person { Id = Guid.NewGuid(), GivenName = command.GivenName, Gender = Gender.Male, SurName = command.SurName };
        var response = new AddPersonCommandResponse
        {
            GivenName = person.GivenName,
            SurName = person.SurName,
            Gender = GenderEnum.Male,
            BirthDate = person.BirthDate,
            BirthLocation = person.BirthLocation,
            DeathDate = person.DeathDate,
            DeathLocation = person.DeathLocation
        };

        _mapperMock.Setup(mapper => mapper.Map<Person>(command)).Returns(person);
        _personRepositoryMock.Setup(repo => repo.AddAsync(person)).ReturnsAsync(person);
        _mapperMock.Setup(mapper => mapper.Map<AddPersonCommandResponse>(person)).Returns(response);

        // Act  
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert  
        Assert.NotNull(result);
        Assert.Equal(command.GivenName, result.GivenName);
        _unitOfWorkMock.Verify(uow => uow.Save(It.IsAny<CancellationToken>()), Times.Once);
    }
}
