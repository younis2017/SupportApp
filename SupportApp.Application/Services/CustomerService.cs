using SupportApp.Application.Abstractions.Services;
using SupportApp.Application.Abstractions.Persistence;
using SupportApp.Dtos;
using SupportApp.Domain.Entities;

namespace SupportApp.Application.Services
    {
    public class CustomerService: ICustomerService
        {
        private readonly ICustomerRepository _repo;

        public CustomerService (ICustomerRepository repo)
            {
            _repo = repo;
            }

        public async Task<CustomerReadDto> CreateAsync (CustomerCreateDto dto)
            {
            var customer = new Customer { Name = dto.Name, Email = dto.Email };
            await _repo.AddAsync(customer);
            await _repo.SaveChangesAsync();

            return new CustomerReadDto
                {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
                };
            }

        public async Task DeleteAsync (int id) => await _repo.DeleteByIdAsync(id);

        public async Task<CustomerReadDto?> GetByIdAsync (int id)
            {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null) return null;

            return new CustomerReadDto
                {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
                };
            }

        public async Task<IReadOnlyList<CustomerReadDto>> GetAllAsync ()
            {
            var customers = await _repo.GetAllAsync();
            return customers.Select(c => new CustomerReadDto
                {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email
                }).ToList();
            }

        public async Task UpdateAsync (int id, CustomerUpdateDto dto)
            {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null) throw new Exception("Customer not found");

            customer.Name = dto.Name;
            customer.Email = dto.Email;
            _repo.Update(customer);
            await _repo.SaveChangesAsync();
            }
        }
    }