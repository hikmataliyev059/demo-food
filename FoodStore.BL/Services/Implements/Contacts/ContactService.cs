using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Contacts;
using FoodStore.BL.Helpers.Email;
using FoodStore.BL.Helpers.Exceptions.Common;
using FoodStore.BL.Services.Interfaces.Contacts;
using FoodStore.BL.Services.Interfaces.Email;
using FoodStore.Core.Entities.Contacts;
using FoodStore.Core.Repositories.Interfaces.Contacts;

namespace FoodStore.BL.Services.Implements.Contacts;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly IMailService _mailService;
    private readonly IMapper _mapper;

    public ContactService(IContactRepository contactRepository, IMailService mailService, IMapper mapper)
    {
        _contactRepository = contactRepository;
        _mailService = mailService;
        _mapper = mapper;
    }

    public async Task<ContactDto> GetContactByIdAsync(int id)
    {
        if (id <= 0) throw new NegativeIdException();

        var contact = await _contactRepository.GetByIdAsync(id);
        if (contact == null) throw new NotFoundException<Contact>();

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<ContactDto> CreateContactAsync(ContactDto createContactDto)
    {
        var contact = _mapper.Map<Contact>(createContactDto);
        var addedContact = await _contactRepository.AddAsync(contact);
        await _contactRepository.SaveChangesAsync();

        var adminSubject = "New Message from Contact Form";
        var adminBody = $"You have received a new message from {createContactDto.FullName}.\n\n" +
                        $"Email: {createContactDto.Email}\n" +
                        $"Phone Number: {createContactDto.PhoneNumber}\n\n" +
                        $"Subject: {createContactDto.Subject}\n\n" +
                        $"Message: {createContactDto.Message}";

        var adminMailRequest = new MailRequest
        {
            ToEmail = "marcelo850948@gmail.com",
            Subject = adminSubject,
            Body = adminBody
        };

        var userSubject = "Your message has been successfully sent!";
        var userBody = "Thank you for contacting us. We will get back to you shortly.";

        var userMailRequest = new MailRequest
        {
            ToEmail = createContactDto.Email,
            Subject = userSubject,
            Body = userBody
        };

        await Task.WhenAll(_mailService.SendEmailAsync(adminMailRequest), _mailService.SendEmailAsync(userMailRequest));

        return _mapper.Map<ContactDto>(addedContact);
    }


    public async Task<ContactUpdateDto> UpdateContactAsync(int id, ContactUpdateDto contactUpdateDto)
    {
        if (id <= 0) throw new NegativeIdException();

        var contact = await _contactRepository.GetByIdAsync(id);
        if (contact == null) throw new NotFoundException<Contact>();

        _mapper.Map(contactUpdateDto, contact);
        await _contactRepository.Update(contact);
        await _contactRepository.SaveChangesAsync();
        return _mapper.Map<ContactUpdateDto>(contact);
    }

    public async Task DeleteContactAsync(int id)
    {
        if (id <= 0) throw new NegativeIdException();

        var contact = await _contactRepository.GetByIdAsync(id);
        if (contact == null) throw new NotFoundException<Contact>();

        await _contactRepository.Delete(contact);
        await _contactRepository.SaveChangesAsync();
    }
}