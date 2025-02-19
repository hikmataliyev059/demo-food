using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Blogs;
using FoodStore.BL.Helpers.Exceptions.Common;
using FoodStore.BL.Services.Interfaces.Blogs;
using FoodStore.BL.Services.Interfaces.File;
using FoodStore.Core.Entities.Blogs;
using FoodStore.Core.Entities.Categories;
using FoodStore.Core.Entities.Products;
using FoodStore.Core.Enums;
using FoodStore.Core.Repositories.Interfaces.Blogs;
using FoodStore.Core.Repositories.Interfaces.Categories;
using FoodStore.Core.Repositories.Interfaces.Products;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.BL.Services.Implements.Blogs;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IFileStorage _fileStorage;
    private readonly IMapper _mapper;

    public ArticleService(IArticleRepository articleRepository, IMapper mapper, ICategoryRepository categoryRepository,
        IAuthorRepository authorRepository, ITagRepository tagRepository, IFileStorage fileStorage)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _authorRepository = authorRepository;
        _tagRepository = tagRepository;
        _fileStorage = fileStorage;
    }

    public async Task<ArticleGetDto> CreateArticleAsync(ArticleCreateDto articleCreateDto)
    {
        var article = _mapper.Map<Article>(articleCreateDto);
        article.PublishDate = DateTime.UtcNow;

        var author = await _authorRepository.GetByIdAsync(articleCreateDto.AuthorId);
        if (author == null) throw new NotFoundException<Author>();
        article.Author = author;

        var categoryId = await _categoryRepository.GetByIdAsync(articleCreateDto.CategoryId);
        if (categoryId == null) throw new NotFoundException<Category>();
        article.Category = categoryId;

        var existingTags = await _tagRepository.GetWhere(tag => articleCreateDto.TagIds.Contains(tag.Id)).ToListAsync();
        if (existingTags.Count != articleCreateDto.TagIds.Count) throw new NotFoundException<Tag>();

        if (articleCreateDto.TagIds.Any())
        {
            article.ArticleTags = articleCreateDto.TagIds.Select(tagId => new ArticleTag { TagId = tagId }).ToList();
        }

        if (articleCreateDto.Image != null)
        {
            var imageUrl = await _fileStorage.UploadFileAsync(articleCreateDto.Image, StorageContainer.Article);
            article.ImageUrl = imageUrl;
        }

        var createdArticle = await _articleRepository.AddAsync(article);
        await _articleRepository.SaveChangesAsync();

        var tagNames = await _tagRepository.GetWhere(tag => articleCreateDto.TagIds.Contains(tag.Id))
            .Select(tag => tag.Name)
            .ToListAsync();

        var articleGetDto = _mapper.Map<ArticleGetDto>(createdArticle);
        articleGetDto.TagNames = tagNames;

        return articleGetDto;
    }

    public async Task<ArticleGetDto> GetArticleByIdAsync(int id)
    {
        var article = await _articleRepository.GetAll()
            .Include(a => a.ArticleTags)
            .ThenInclude(at => at.Tag)
            .Include(a => a.Category)
            .Include(a => a.Author)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (article == null) throw new NotFoundException<Article>();
        return _mapper.Map<ArticleGetDto>(article);
    }

    public async Task<IEnumerable<ArticleGetDto>> GetAllArticlesAsync()
    {
        var articles = await _articleRepository.GetAll()
            .Include(a => a.ArticleTags)
            .ThenInclude(at => at.Tag)
            .Include(a => a.Category)
            .Include(a => a.Author)
            .ToListAsync();

        if (articles == null || !articles.Any()) throw new NotFoundException<Article>();

        var articleGetDtos = _mapper.Map<IEnumerable<ArticleGetDto>>(articles);
        return articleGetDtos;
    }

    public async Task<ArticleGetDto> UpdateArticleAsync(int id, ArticleUpdateDto articleUpdateDto)
    {
        var article = await _articleRepository.GetAll()
            .Include(a => a.ArticleTags)
            .ThenInclude(at => at.Tag)
            .Include(a => a.Category)
            .Include(a => a.Author)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (article == null) throw new NotFoundException<Article>();

        if (articleUpdateDto.Image != null)
        {
            if (!string.IsNullOrEmpty(article.ImageUrl))
            {
                var oldFileName = Path.GetFileName(article.ImageUrl);
                await _fileStorage.DeleteFileAsync(oldFileName, StorageContainer.Article);
            }

            var imageUrl = await _fileStorage.UploadFileAsync(articleUpdateDto.Image, StorageContainer.Article);
            article.ImageUrl = imageUrl;
        }

        _mapper.Map(articleUpdateDto, article);
        await _articleRepository.Update(article);
        await _articleRepository.SaveChangesAsync();

        return _mapper.Map<ArticleGetDto>(article);
    }

    public async Task DeleteArticleAsync(int id)
    {
        var article = await _articleRepository.GetByIdAsync(id);
        if (article == null) throw new NotFoundException<Article>();

        if (!string.IsNullOrEmpty(article.ImageUrl))
        {
            var fileName = Path.GetFileName(article.ImageUrl);
            await _fileStorage.DeleteFileAsync(fileName, StorageContainer.Article);
        }

        await _articleRepository.Delete(article);
        await _articleRepository.SaveChangesAsync();
    }
}