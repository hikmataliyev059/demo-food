﻿using FoodStore.Core.Entities.Common;

namespace FoodStore.Core.Entities.Contacts;

public class Contact : BaseEntity
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
}