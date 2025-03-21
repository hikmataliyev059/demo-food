﻿namespace FoodStore.BL.Helpers.DTOs.Blogs;

public record CommentCreateDto
{
    public string Content { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public int ArticleId { get; set; }
}