using MediatR;
using Storied.Application.Features.Person.Queries;
using Storied.Application.Models;
using AutoMapper;
using Storied.Application.Repositories;

namespace Storied.Application.Features.Person.Handlers
{
    /// <summary>  
    /// Handles the query to retrieve all people.  
    /// </summary>  
    public sealed class GetAllPeopleQueryHandler : IRequestHandler<GetAllPeopleQuery, List<GetAllPeopleQueryResponse>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        /// <summary>  
        /// Initializes a new instance of the <see cref="GetAllPeopleQueryHandler"/> class.  
        /// </summary>  
        /// <param name="personRepository">The repository for accessing person data.</param>  
        /// <param name="mapper">The mapper for transforming entities to DTOs.</param>  
        public GetAllPeopleQueryHandler(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        /// <summary>  
        /// Handles the query to retrieve all people.  
        /// </summary>  
        /// <param name="request">The query request.</param>  
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>  
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of people responses.</returns>  
        public async Task<List<GetAllPeopleQueryResponse>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
        {
            var persons = await _personRepository.GetAllPersons(cancellationToken: cancellationToken);
            return _mapper.Map<List<GetAllPeopleQueryResponse>>(persons);
        }
    }
}
