using SupportApp.Dtos;

namespace SupportApp.Application.Abstractions.Services;

public interface ICommentService
    {
    Task<CommentReadDto> CreateAsync (CommentCreateDto dto);
    Task<CommentReadDto?> GetByIdAsync (int id);
    Task<IReadOnlyList<CommentReadDto>> GetAllAsync ();
    Task UpdateAsync (int id, CommentUpdateDto dto);
    Task DeleteAsync (int id);
    }