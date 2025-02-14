using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Blogs;
using FoodStore.BL.Helpers.Exceptions.Common;
using FoodStore.BL.Helpers.Extensions.File;
using FoodStore.BL.Services.Interfaces;
using FoodStore.Core.Entities.Blogs;
using FoodStore.Core.Entities.Categories;
using FoodStore.Core.Entities.Products;
using FoodStore.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.BL.Services.Implements;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;

    public ArticleService(IArticleRepository articleRepository, IMapper mapper, ICategoryRepository categoryRepository,
        IAuthorRepository authorRepository, ITagRepository tagRepository, IWebHostEnvironment env)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _authorRepository = authorRepository;
        _tagRepository = tagRepository;
        _env = env;
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

        _mapper.Map(articleUpdateDto, article);
        await _articleRepository.Update(article);
        await _articleRepository.SaveChangesAsync();

        return _mapper.Map<ArticleGetDto>(article);
    }
    
    public async Task DeleteArticleAsync(int id)
    {
        var article = await _articleRepository.GetByIdAsync(id);
        if (article == null) throw new NotFoundException<Article>();

        FileUploadExtensions.Delete(_env.WebRootPath, article.ImageUrl);

        await _articleRepository.Delete(article);
        await _articleRepository.SaveChangesAsync();
    }
}