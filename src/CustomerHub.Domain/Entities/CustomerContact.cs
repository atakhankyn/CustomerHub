public class CustomerContact
{
    public Guid Id { get; set; } =  Guid.NewGuid(); //Primary Key
    public Guid CustomerId { get; set; } //Foreign Key
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public ContactRole? Role { get; set; }

    public Customer Customer { get; set; } = null!;
}