using AutoMapper;
using Storied.Application.Models;

namespace Storied.Application.Mappers
{
    /// <summary>  
    /// Provides mapping configuration for converting a Person entity to a GetPersonByIdQueryResponse.  
    /// </summary>  
    public sealed class GetPersonByIdMapper : Profile
    {
        /// <summary>  
        /// Initializes a new instance of the <see cref="GetPersonByIdMapper"/> class.  
        /// Configures the mapping between <see cref="Domain.Entities.Person"/> and <see cref="GetPersonByIdQueryResponse"/>.  
        /// </summary>  
        public GetPersonByIdMapper()
        {
            CreateMap<Domain.Entities.Person, GetPersonByIdQueryResponse>();
        }
    }
}
