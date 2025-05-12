using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Storied.Application.Features.Person.Handlers;
using Storied.Application.Features.Person.Queries;
using Storied.Application.Models;
using Storied.Application.Repositories;
using Storied.Domain.Common.Enums;
using Storied.Domain.Entities;

namespace Storied.Test
{
    /// </summary>  
    public class GetAllPeopleQueryHandlerTests
    {
        private readonly Mock<IPersonRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetAllPeopleQueryHandler _handler;

        /// <summary>  
        /// Initializes a new instance of the <see cref="GetAllPeopleQueryHandlerTests"/> class.  
        /// </summary>  
        public GetAllPeopleQueryHandlerTests()
        {
            _repositoryMock = new Mock<IPersonRepository>();
            _mapperMock = new Mock<IMapper>();
            Mock<ILogger<GetAllPeopleQueryHandler>> loggerMock = new();
            _handler = new GetAllPeopleQueryHandler(_repositoryMock.Object, _mapperMock.Object, loggerMock.Object);
        }

        /// <summary>  
        /// Tests that the <see cref="GetAllPeopleQueryHandler.Handle"/> method returns an empty list when no persons exist.  
        /// </summary>  
        [Fact]
        public async Task Handle_ShouldReturnNull_WhenNoPersonsExist()
        {
            // Arrange  
            _repositoryMock.Setup(repo => repo.GetAllPersons(It.IsAny<CancellationToken>())).ReturnsAsync(new List<Person>());

            // Act  
            var result = await _handler.Handle(new GetAllPeopleQuery(), CancellationToken.None);

            // Assert  
            Assert.Null(result);
        }

        /// <summary>  
        /// Tests that the <see cref="GetAllPeopleQueryHandler.Handle"/> method returns a list of persons when persons exist.  
        /// </summary>  
        [Fact]
        public async Task Handle_ShouldReturnListOfPersons_WhenPersonsExist()
        {
            // Arrange  
            var persons = new List<Person>
           {
               new() { Id = Guid.NewGuid(), GivenName = "John", SurName = "Doe", Gender = Gender.Male },
               new() { Id = Guid.NewGuid(), GivenName = "Jane", SurName = "Smith", Gender = Gender.Female }
           };
            var response = new List<GetAllPeopleQueryResponse>
           {
               new() { Id = persons[0].Id, GivenName = persons[0].GivenName },
               new() { Id = persons[1].Id, GivenName = persons[1].GivenName }
           };

            _repositoryMock.Setup(repo => repo.GetAllPersons(It.IsAny<CancellationToken>())).ReturnsAsync(persons);
            _mapperMock.Setup(mapper => mapper.Map<List<GetAllPeopleQueryResponse>>(persons)).Returns(response);

            // Act  
            var result = await _handler.Handle(new GetAllPeopleQuery(), CancellationToken.None);

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}
