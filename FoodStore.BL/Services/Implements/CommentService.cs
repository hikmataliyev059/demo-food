using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Blogs;
using FoodStore.BL.Helpers.Exceptions.Common;
using FoodStore.BL.Services.Interfaces;
using FoodStore.Core.Entities.Blogs;
using FoodStore.Core.Repositories.Interfaces;

namespace FoodStore.BL.Services.Implements;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository commentRepository, IMapper mapper, IArticleRepository articleRepository)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
        _articleRepository = articleRepository;
    }

    public async Task<CommentGetDto> AddCommentAsync(CommentCreateDto commentCreateDto)
    {
        var comment = _mapper.Map<Comment>(commentCreateDto);
        comment.CommentDate = DateTime.UtcNow;

        var createdComment = await _commentRepository.AddAsync(comment);
        await _commentRepository.SaveChangesAsync();

        return _mapper.Map<CommentGetDto>(createdComment);
    }

    public async Task<IEnumerable<CommentGetDto>> GetCommentsByArticleIdAsync(int articleId)
    {
        var article = await _articleRepository.GetByIdAsync(articleId);
        if (article == null) throw new NotFoundException<Article>();

        var comments = await _commentRepository.GetCommentsByArticleIdAsync(articleId);
        if (comments == null) throw new NotFoundException<Article>();
        return _mapper.Map<List<CommentGetDto>>(comments);
    }

    public async Task<CommentGetDto> UpdateCommentAsync(int id, CommentUpdateDto commentUpdateDto)
    {
        if (id <= 0) throw new NegativeIdException();

        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null) throw new NotFoundException<Comment>();

        _mapper.Map(commentUpdateDto, comment);
        await _commentRepository.Update(comment);
        await _commentRepository.SaveChangesAsync();

        return _mapper.Map<CommentGetDto>(comment);
    }

    public async Task DeleteCommentAsync(int id)
    {
        if (id <= 0) throw new NegativeIdException();

        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null) throw new NotFoundException<Comment>();

        await _commentRepository.Delete(comment);
        await _commentRepository.SaveChangesAsync();
    }
}