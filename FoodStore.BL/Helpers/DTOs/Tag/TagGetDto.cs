﻿namespace FoodStore.BL.Helpers.DTOs.Tag;

public record TagGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
}