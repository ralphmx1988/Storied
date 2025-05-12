using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
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
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetPersonByIdQueryHandler _handler;

    /// <summary>  
    /// Initializes a new instance of the <see cref="GetPersonByIdQueryHandlerTests"/> class.  
    /// </summary>  
    /// <remarks>  
    /// This constructor sets up the necessary mocks and initializes the handler  
    /// to be tested. The mocks include the repository, mapper, and logger dependencies.  
    /// </remarks>  
    public GetPersonByIdQueryHandlerTests()
    {
        _repositoryMock = new Mock<IPersonRepository>();
        _mapperMock = new Mock<IMapper>();
        var loggerMock = new Mock<ILogger<GetPersonByIdQueryHandler>>();
        _handler = new GetPersonByIdQueryHandler(_repositoryMock.Object, _mapperMock.Object, loggerMock.Object);
    }

    /// <summary>  
    /// Tests that the <see cref="GetPersonByIdQueryHandler.Handle"/> method returns null  
    /// when the person is not found in the repository.  
    /// </summary>  
    /// <returns>A task that represents the asynchronous operation.</returns>  
    [Fact]
    public async Task Handle_ShouldReturnNull_WhenPersonNotFound()
    {
        // Arrange  
        var query = new GetPersonByIdQuery(Guid.NewGuid());
        _repositoryMock.Setup(repo => repo.GetByIdAsync(query.Id, It.IsAny<CancellationToken>())).ReturnsAsync((Person)null!);

        // Act  
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert  
        Assert.Null(result);
    }

    /// <summary>  
    /// Tests that the <see cref="GetPersonByIdQueryHandler.Handle"/> method returns a person  
    /// when the person exists in the repository.  
    /// </summary>  
    /// <returns>A task that represents the asynchronous operation.</returns>  
    [Fact]
    public async Task Handle_ShouldReturnPerson_WhenPersonExists()
    {
        // Arrange  
        var query = new GetPersonByIdQuery(Guid.NewGuid());
        var person = new Person { Id = query.Id, GivenName = "John", Gender = Gender.Male, SurName = "Doe" };
        var response = new GetPersonByIdQueryResponse { Id = person.Id, GivenName = person.GivenName };

        _repositoryMock.Setup(repo => repo.GetByIdAsync(query.Id, It.IsAny<CancellationToken>())).ReturnsAsync(person);
        _mapperMock.Setup(mapper => mapper.Map<GetPersonByIdQueryResponse>(person)).Returns(response);

        // Act  
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert  
        Assert.NotNull(result);
        Assert.Equal(query.Id, result.Id);
    }
}

