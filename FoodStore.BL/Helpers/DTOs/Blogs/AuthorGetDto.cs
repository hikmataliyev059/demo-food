﻿namespace FoodStore.BL.Helpers.DTOs.Blogs;

public record AuthorGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public string ImageUrl { get; set; }
}