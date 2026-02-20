
    using global::SupportApp.Application.Abstractions.Persistence;
    using global::SupportApp.Application.Abstractions.Services;
    using global::SupportApp.Domain.Entities;
    using global::SupportApp.Dtos;

    namespace SupportApp.Application.Services
        {
        public class AgentService: IAgentService
            {
            private readonly IAgentRepository _repo;

            public AgentService (IAgentRepository repo)
                {
                _repo = repo;
                }

            public async Task<AgentReadDto> CreateAsync (AgentCreateDto dto)
                {
                if (await _repo.EmailExistsAsync(dto.Email , int.MaxValue))
                    throw new Exception("Email already exists");

                var agent = new Agent
                    {
                    DisplayName = dto.DisplayName,
                    Email = dto.Email
                    };

                await _repo.AddAsync(agent);
                await _repo.SaveChangesAsync();

                return new AgentReadDto
                    {
                    Id = agent.Id,
                    DisplayName = agent.DisplayName,
                    Email = agent.Email
                    };
                }

            public async Task<AgentReadDto?> GetByIdAsync (int id)
                {
                var agent = await _repo.GetByIdAsync(id);
                if (agent == null) return null;

                return new AgentReadDto
                    {
                    Id = agent.Id,
                    DisplayName = agent.DisplayName,
                    Email = agent.Email
                    };
                }

            public async Task<IReadOnlyList<AgentReadDto>> GetAllAsync ()
                {
                var agents = await _repo.GetAllAsync();
                return agents.Select(a => new AgentReadDto
                    {
                    Id = a.Id,
                    DisplayName = a.DisplayName,
                    Email = a.Email
                    }).ToList();
                }

            public async Task UpdateAsync (int id, AgentCreateDto dto)
                {
                var agent = await _repo.GetByIdAsync(id);
                if (agent == null) throw new Exception("Agent not found");

                if (await _repo.EmailExistsAsync(dto.Email, id))
                    throw new Exception("Email already exists");

                agent.DisplayName = dto.DisplayName;
                agent.Email = dto.Email;

                _repo.Update(agent);
                await _repo.SaveChangesAsync();
                }

            public async Task DeleteAsync (int id)
                {
                await _repo.DeleteByIdAsync(id);
                await _repo.SaveChangesAsync();
                }

            public async Task<bool> EmailExistsAsync (string email, int? excludeId = null)
                {
                return await _repo.EmailExistsAsync(email, (int)excludeId);
                }
            }
        }
    
