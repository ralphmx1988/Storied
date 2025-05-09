using AutoMapper;
using FluentAssertions;
using Moq;
using Storied.Application.Common.Enums;
using Storied.Application.Features.Person.Commands;
using Storied.Application.Features.Person.Handlers;
using Storied.Application.Models;
using Storied.Application.Repositories;
using Storied.Domain.Common.Enums;
using Storied.Domain.Entities;

namespace Storied.Test;

/// <summary>
/// Unit tests for <see cref="AddPersonCommandHandler"/>.
/// </summary>
public class AddPersonCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly AddPersonCommandHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddPersonCommandHandlerTests"/> class.
    /// </summary>
    public AddPersonCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _personRepositoryMock = new Mock<IPersonRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new AddPersonCommandHandler(_unitOfWorkMock.Object, _personRepositoryMock.Object, _mapperMock.Object);
    }

    /// <summary>
    /// Tests that the <see cref="AddPersonCommandHandler.Handle"/> method adds a person when the command is valid.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task Handle_ShouldAddPerson_WhenCommandIsValid()
    {
        // Arrange  
        var command = new AddPersonCommand(
            GivenName: "John",
            SurName: "Doe",
            Gender: "Male",
            BirthDate: "1990-01-01",
            BirthLocation: "New York",
            DeathDate: null,
            DeathLocation: null
        );

        var person = new Person
        {
            Id = Guid.NewGuid(),
            GivenName = "John",
            SurName = "Doe",
            Gender = Gender.Male,
            BirthDate = new DateTime(1990, 1, 1),
            BirthLocation = "New York"
        };

        var response = new AddPersonCommandResponse
        {
            GivenName = "John",
            SurName = "Doe",
            Gender = GenderEnum.Male,
            BirthDate = new DateTime(1990, 1, 1),
            BirthLocation = "New York"
        };

        _mapperMock.Setup(m => m.Map<Person>(command)).Returns(person);
        _mapperMock.Setup(m => m.Map<AddPersonCommandResponse>(person)).Returns(response);

        // Act  
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert  
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(response);

        _personRepositoryMock.Verify(r => r.AddAsync(person), Times.Once);
        _unitOfWorkMock.Verify(u => u.Save(It.IsAny<CancellationToken>()), Times.Once);
    }
}
