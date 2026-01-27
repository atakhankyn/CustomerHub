using Microsoft.EntityFrameworkCore;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerHubDbContext _context;

    public CustomerRepository(CustomerHubDbContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }
    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _context.Customers.FindAsync(id);
    }
    public async Task<Customer?> GetByTCKNAsync(string tcknOrVkn)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.TCKNOrVKN == tcknOrVkn);
    }
    public async Task<List<Customer>> SearchByNameAsync(string name)
    {
        return await _context.Customers.Where(c => c.Name.Contains(name)).ToListAsync();
    }
    public async Task<bool> ExistsWithTCKNAsync(string tcknOrVkn)
    {
        return await _context.Customers.AnyAsync(c => c.TCKNOrVKN == tcknOrVkn);
    }
    public async Task<bool> ExistsWithTCKNAsync(string tcknOrVkn, Guid excludeId)
    {
        return await _context.Customers.AnyAsync(c => c.TCKNOrVKN == tcknOrVkn && c.Id != excludeId);
    }
    public async Task AddAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }
}