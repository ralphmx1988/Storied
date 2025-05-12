using AutoMapper;
using Storied.Application.Models;

namespace Storied.Application.Mappers
{
    /// <summary>  
    /// Mapper profile for updating a person.  
    /// </summary>  
    public sealed class RecordBirthCommandMapper : Profile
    {
        /// <summary>  
        /// Initializes a new instance of the <see cref="RecordBirthCommandMapper"/> class.  
        /// Configures the mapping between <see cref="Domain.Entities.Person"/> and <see cref="RecordBirthCommandResponse"/>.  
        /// </summary>  
        public RecordBirthCommandMapper()
        {
            CreateMap<Domain.Entities.Person, RecordBirthCommandResponse>();
        }
    }
}
