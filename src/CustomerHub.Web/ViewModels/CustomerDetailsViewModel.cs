public class CustomerDetailsViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public CustomerType Type { get; set; }
    public string TypeText { get; set; } = string.Empty;
    public CustomerStatus Status { get; set; }
    public string StatusText { get; set; } = string.Empty;
    public string StatusBadgeClass { get; set; } = string.Empty;
    public string TCKNOrVKN { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? AddressCity { get; set; }
    public string? AddressLine { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool CanBeEdited { get; set; }
    public bool CanBeSuspended { get; set; }
    public bool CanBeActivated { get; set; }
    public bool CanBeClosed { get; set; }
}