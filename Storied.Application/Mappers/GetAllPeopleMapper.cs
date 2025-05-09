using AutoMapper;
using Storied.Application.Models;

namespace Storied.Application.Mappers
{
    /// <summary>  
    /// Provides mapping configuration for retrieving all people.  
    /// </summary>  
    public sealed class GetAllPeopleMapper : Profile
    {
        /// <summary>  
        /// Initializes a new instance of the <see cref="GetAllPeopleMapper"/> class.  
        /// Configures the mapping between <see cref="Domain.Entities.Person"/> and <see cref="GetAllPeopleQueryResponse"/>.  
        /// </summary>  
        public GetAllPeopleMapper()
        {
            CreateMap<Domain.Entities.Person, GetAllPeopleQueryResponse>();
        }
    }
}
