using AutoMapper;
using Storied.Application.Models;

namespace Storied.Application.Mappers
{
    /// <summary>  
    /// Mapper profile for updating a person.  
    /// </summary>  
    public sealed class UpdatePersonCommandMapper : Profile
    {
        /// <summary>  
        /// Initializes a new instance of the <see cref="UpdatePersonCommandMapper"/> class.  
        /// Configures the mapping between <see cref="Domain.Entities.Person"/> and <see cref="UpdatePersonCommandResponse"/>.  
        /// </summary>  
        public UpdatePersonCommandMapper()
        {
            CreateMap<Domain.Entities.Person, UpdatePersonCommandResponse>();
        }
    }
}
