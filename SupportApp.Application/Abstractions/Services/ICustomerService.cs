using SupportApp.Dtos;

namespace SupportApp.Application.Abstractions.Services;

public interface ICustomerService
    {
    Task<CustomerReadDto> CreateAsync (CustomerCreateDto dto);
    Task<CustomerReadDto?> GetByIdAsync (int id);
    Task<IReadOnlyList<CustomerReadDto>> GetAllAsync ();
    Task UpdateAsync (int id, CustomerUpdateDto dto);
    Task DeleteAsync (int id);
    }