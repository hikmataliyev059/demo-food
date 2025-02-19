using FoodStore.BL.Helpers.DTOs.Contacts;
using FoodStore.BL.Services.Interfaces.Contact;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers.Contact;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContactById(int id)
    {
        var contact = await _contactService.GetContactByIdAsync(id);
        return Ok(contact);
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact([FromBody] ContactDto createContactDto)
    {
        var createdContact = await _contactService.CreateContactAsync(createContactDto);
        return StatusCode(201, createdContact);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContact(int id, [FromBody] ContactUpdateDto contactUpdateDto)
    {
        var updatedContact = await _contactService.UpdateContactAsync(id, contactUpdateDto);
        return Ok(updatedContact);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(int id)
    {
        await _contactService.DeleteContactAsync(id);
        return NoContent();
    }
}