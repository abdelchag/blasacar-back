using AutoMapper;
using blasa.travel.Core.Domain.Entities;
using blasa.travel.web.Models;

namespace blasa.travel.web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
             CreateMap<Travel, TravelModel>();
             CreateMap<TravelModel, Travel>();
        }
    }
}
