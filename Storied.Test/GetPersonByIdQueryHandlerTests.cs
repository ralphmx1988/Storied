using AutoMapper;
using FluentAssertions;
using Moq;
using Storied.Application.Common.Exceptions;
using Storied.Application.Features.Person.Handlers;
using Storied.Application.Features.Person.Queries;
using Storied.Application.Models;
using Storied.Application.Repositories;
using Storied.Domain.Common.Enums;
using Storied.Domain.Entities;

namespace Storied.Test;

/// <summary>
/// Unit tests for the <see cref="GetPersonByIdQueryHandler"/> class.
/// </summary>
public class GetPersonByIdQueryHandlerTests
{
    private readonly Mock<IPersonRepository> _repositoryMock;
    private readonly GetPersonByIdQueryHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPersonByIdQueryHandlerTests"/> class.
    /// </summary>
    public GetPersonByIdQueryHandlerTests()
    {
        var mapperMock = new Mock<IMapper>();
        _repositoryMock = new Mock<IPersonRepository>();
        _handler = new GetPersonByIdQueryHandler(_repositoryMock.Object, mapperMock.Object);
    }

    /// <summary>  
    /// Tests that the <see cref="GetPersonByIdQueryHandler.Handle"/> method returns a person when the person exists.  
    /// </summary>  
    /// <returns>A task that represents the asynchronous operation.</returns>  
    [Fact]
    public async Task Handle_ShouldReturnPerson_WhenPersonExists()
    {
        // Arrange  
        var personId = Guid.NewGuid();
        var person = new Person
        {
            Id = personId,
            GivenName = "John",
            SurName = "Doe",
            Gender = Gender.Male,
            BirthDate = new DateTime(1990, 1, 1),
            BirthLocation = "New York"
        };

        var expectedResponse = new GetPersonByIdQueryResponse
        {
            Id = personId,
            GivenName = "John",
            SurName = "Doe",
            Gender = "Male",
            BirthDate = new DateTime(1990, 1, 1),
            BirthLocation = "New York"
        };

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<GetPersonByIdQueryResponse>(person))
            .Returns(expectedResponse);

        _repositoryMock.Setup(r => r.GetByIdAsync(personId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(person);

        var handler = new GetPersonByIdQueryHandler(_repositoryMock.Object, mapperMock.Object);
        var query = new GetPersonByIdQuery(personId);

        // Act  
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert  
        result.Should().NotBeNull();
        result.Id.Should().Be(personId);
        result.GivenName.Should().Be("John");
        result.SurName.Should().Be("Doe");
        result.Gender.Should().Be("Male");
        result.BirthDate.Should().Be(new DateTime(1990, 1, 1));
        result.BirthLocation.Should().Be("New York");


    }
}
