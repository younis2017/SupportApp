using SupportApp.Dtos;

namespace SupportApp.Application.Abstractions.Services;

public interface IAgentService
    {
    Task<AgentReadDto> CreateAsync (AgentCreateDto dto);
    Task<AgentReadDto?> GetByIdAsync (int id);
    Task<IReadOnlyList<AgentReadDto>> GetAllAsync ();
    Task DeleteAsync (int id);
    }