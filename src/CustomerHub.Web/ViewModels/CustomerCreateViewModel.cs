public class CustomerCreateViewModel
{
    public CustomerType? Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TCKNOrVKN { get; set; } = string.Empty;
    public string? AddressCity { get; set; }
    public string? AddressLine { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}