using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Tag;
using FoodStore.BL.Helpers.Exceptions.Common;
using FoodStore.BL.Helpers.Extensions.Slug;
using FoodStore.BL.Services.Interfaces.Products;
using FoodStore.Core.Entities.Products;
using FoodStore.Core.Repositories.Interfaces.Products;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.BL.Services.Implements.Products;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;

    public TagService(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    public async Task<TagGetDto?> GetByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new NegativeIdException();
        }

        var tag = await _tagRepository.GetByIdAsync(id);
        return tag != null ? _mapper.Map<TagGetDto>(tag) : throw new NotFoundException<Tag>();
    }

    public async Task<IEnumerable<TagGetDto>> GetAllAsync()
    {
        var tags = await _tagRepository.GetAll().ToListAsync();
        var mappedTags = tags.Select(tag => _mapper.Map<TagGetDto>(tag));
        return mappedTags;
    }

    public async Task CreateAsync(TagCreateDto tagCreateDto)
    {
        if (await _tagRepository.Table.AnyAsync(c => c.Name == tagCreateDto.Name))
        {
            throw new AlreadyExistsException<Tag>();
        }

        var tag = _mapper.Map<Tag>(tagCreateDto);
        await _tagRepository.AddAsync(tag);
        await _tagRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, TagUpdateDto tagUpdateDto)
    {
        if (id <= 0)
        {
            throw new NegativeIdException();
        }

        if (await _tagRepository.Table.AnyAsync(c => c.Name == tagUpdateDto.Name && c.Id != id))
        {
            throw new AlreadyExistsException<Tag>();
        }

        var existingTag = await _tagRepository.GetByIdAsync(id);
        if (existingTag == null) throw new NotFoundException<Tag>();

        _mapper.Map(tagUpdateDto, existingTag);
        existingTag.Slug = SlugHelper.CreateSlug(existingTag.Name);
        await _tagRepository.Update(existingTag);
        await _tagRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0)
        {
            throw new NegativeIdException();
        }

        var existingTag = await _tagRepository.GetByIdAsync(id);
        if (existingTag == null) throw new NotFoundException<Tag>();

        await _tagRepository.Delete(existingTag);
        await _tagRepository.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        if (id <= 0)
        {
            throw new NegativeIdException();
        }

        var existingTag = await _tagRepository.GetByIdAsync(id);
        if (existingTag == null) throw new NotFoundException<Tag>();

        await _tagRepository.SoftDelete(existingTag);
        await _tagRepository.SaveChangesAsync();
    }
}