using Microsoft.EntityFrameworkCore;

public class CustomerHubDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerContact> CustomerContacts { get; set; }
    public CustomerHubDbContext(DbContextOptions<CustomerHubDbContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerContactConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}