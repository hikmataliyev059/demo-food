using FoodStore.BL.Helpers.DTOs.Contacts;
using FoodStore.Core.Entities.Contacts;

namespace FoodStore.BL.Helpers.Mapper;

public class ContactMapper : ProductMapper
{
    public ContactMapper()
    {
        CreateMap<ContactDto, Contact>().ReverseMap();
        CreateMap<ContactUpdateDto, Contact>()
            .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(src => DateTime.UtcNow)).ReverseMap();
    }
}