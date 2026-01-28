public class CustomerEditViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TCKNOrVKN { get; set; } = string.Empty;
    public CustomerType Type { get; set; }
    public string TypeText { get; set; } = string.Empty;
    public CustomerStatus Status { get; set; }
    public string StatusText { get; set; } = string.Empty;
    public string? AddressCity { get; set; }
    public string? AddressLine { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}