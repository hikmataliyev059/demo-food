using FoodStore.BL.Helpers.DTOs.Contacts;

namespace FoodStore.BL.Services.Interfaces.Contacts;

public interface IContactService
{
    Task<ContactDto> GetContactByIdAsync(int id);

    Task<ContactDto> CreateContactAsync(ContactDto createContactDto);

    Task<ContactUpdateDto> UpdateContactAsync(int id, ContactUpdateDto contactUpdateDto);

    Task DeleteContactAsync(int id);
}