public interface ICustomerRepository
{
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(Guid id);
        Task<Customer?> GetByTCKNAsync(string tcknOrVkn);
        Task<List<Customer>> SearchByNameAsync(string name);
        Task<bool> ExistsWithTCKNAsync(string tcknOrVkn);
        Task<bool> ExistsWithTCKNAsync(string tcknOrVkn, Guid excludeId);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
}