using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Blogs;
using FoodStore.BL.Helpers.Exceptions.Common;
using FoodStore.BL.Services.Interfaces.Blogs;
using FoodStore.BL.Services.Interfaces.File;
using FoodStore.Core.Entities.Blogs;
using FoodStore.Core.Enums;
using FoodStore.Core.Repositories.Interfaces.Blogs;

namespace FoodStore.BL.Services.Implements.Blogs;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IFileStorage _fileStorage;
    private readonly IMapper _mapper;

    public AuthorService(IAuthorRepository authorRepository, IMapper mapper, IFileStorage fileStorage)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _fileStorage = fileStorage;
    }

    public async Task<AuthorGetDto> GetAuthorByIdAsync(int id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null) throw new NotFoundException<Author>();
        return _mapper.Map<AuthorGetDto>(author);
    }

    public Task<IEnumerable<AuthorGetDto>> GetAllAuthorsAsync()
    {
        var authors = _authorRepository.GetAll();
        if (authors == null) throw new NotFoundException<Author>();
        return Task.FromResult(_mapper.Map<IEnumerable<AuthorGetDto>>(authors));
    }

    public async Task<AuthorGetDto> CreateAuthorAsync(AuthorCreateDto authorCreateDto)
    {
        var author = _mapper.Map<Author>(authorCreateDto);
        var createdAuthor = await _authorRepository.AddAsync(author);

        if (authorCreateDto.Image != null)
        {
            var image = await _fileStorage.UploadFileAsync(authorCreateDto.Image, StorageContainer.Author);
            author.ImageUrl = image;
        }

        await _authorRepository.SaveChangesAsync();
        return _mapper.Map<AuthorGetDto>(createdAuthor);
    }

    public async Task<AuthorGetDto> UpdateAuthorAsync(int id, AuthorUpdateDto authorUpdateDto)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null) throw new NotFoundException<Author>();

        if (authorUpdateDto.Image != null)
        {
            if (!string.IsNullOrEmpty(author.ImageUrl))
            {
                var oldFileName = Path.GetFileName(author.ImageUrl);
                await _fileStorage.DeleteFileAsync(oldFileName, StorageContainer.Author);
            }

            var imageUrl = await _fileStorage.UploadFileAsync(authorUpdateDto.Image, StorageContainer.Author);
            author.ImageUrl = imageUrl;
        }

        _mapper.Map(authorUpdateDto, author);
        await _authorRepository.Update(author);
        await _authorRepository.SaveChangesAsync();
        return _mapper.Map<AuthorGetDto>(author);
    }

    public async Task DeleteAuthorAsync(int id)
    {
        if (id <= 0) throw new NegativeIdException();

        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null) throw new NotFoundException<Author>();

        if (!string.IsNullOrEmpty(author.ImageUrl))
        {
            var fileName = Path.GetFileName(author.ImageUrl);
            await _fileStorage.DeleteFileAsync(fileName, StorageContainer.Author);
        }

        await _authorRepository.Delete(author);
        await _authorRepository.SaveChangesAsync();
    }
}