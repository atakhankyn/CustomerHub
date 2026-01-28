using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Property(p => p.TCKNOrVKN).IsRequired().HasMaxLength(20);
        builder.Property(p => p.Type).HasConversion<int>();
        builder.Property(p => p.Status).HasConversion<int>();
        builder.Property(p => p.AddressCity).HasMaxLength(100);
        builder.Property(p => p.AddressLine).HasMaxLength(500);
        builder.Property(c => c.Email).HasMaxLength(100);
        builder.Property(c => c.Phone).HasMaxLength(20);

        builder.HasIndex(p => p.TCKNOrVKN).IsUnique();
        
        //builder.HasMany(c => c.Contacts).WithOne(cc => cc.Customer).HasForeignKey(cc => cc.CustomerId).OnDelete(DeleteBehavior.Cascade);
    }
}