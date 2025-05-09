using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Storied.Application.Features.Person.Queries;
using Storied.Application.Features.Person.Handlers;
using Storied.Application.Models;
using Storied.Application.Repositories;
using Storied.Domain.Entities;
using Xunit;

public class GetAllPeopleQueryHandlerTests
{
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly GetAllPeopleQueryHandler _handler;

    public GetAllPeopleQueryHandlerTests()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _handler = new GetAllPeopleQueryHandler(_personRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllPeople_WhenPeopleExist()
    {
        // Arrange
        var people = new List<Person>
        {
            new Person
            {
                Id = Guid.NewGuid(),
                GivenName = "John",
                SurName = "Doe",
                Gender = Gender.Male,
                BirthDate = new DateTime(1990, 1, 1),
                BirthLocation = "New York"
            },
            new Person
            {
                Id = Guid.NewGuid(),
                GivenName = "Jane",
                SurName = "Smith",
                Gender = Gender.Female,
                BirthDate = new DateTime(1995, 5, 5),
                BirthLocation = "Los Angeles"
            }
        };

        _personRepositoryMock.Setup(r => r.GetAllPersons(It.IsAny<CancellationToken>()))
            .ReturnsAsync(people);

        var query = new GetAllPeopleQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(people, options => options.ExcludingMissingMembers());
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoPeopleExist()
    {
        // Arrange
        _personRepositoryMock.Setup(r => r.GetAllPersons(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Person>());

        var query = new GetAllPeopleQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}
