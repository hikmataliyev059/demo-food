﻿namespace FoodStore.BL.Helpers.DTOs.Contacts;

public class ContactUpdateDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
}