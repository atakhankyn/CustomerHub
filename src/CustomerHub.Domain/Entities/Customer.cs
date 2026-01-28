public class Customer
{
    public Guid Id { get; set; } =  Guid.NewGuid(); //Primary Key
    public CustomerType Type { get; set; }
    public string TCKNOrVKN { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public CustomerStatus Status { get; set; } = CustomerStatus.Active;
    public string? AddressCity { get; set; }
    public string? AddressLine { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    
    //public ICollection<CustomerContact> Contacts { get; set; } = new List<CustomerContact>();

    public void Suspend()
    {
        if (Status == CustomerStatus.Active)
        {
            Status = CustomerStatus.Suspended;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void Activate()
    {
        if (Status == CustomerStatus.Suspended)
        {
            Status = CustomerStatus.Active;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void Close()
    {
        if (Status != CustomerStatus.Closed)
        {
            Status = CustomerStatus.Closed;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public bool CanBeEdited()
    {
        return Status != CustomerStatus.Closed;
    }

    // public void AddContact(CustomerContact contact)
    // {
    //     contact.CustomerId = Id;
    //     Contacts.Add(contact);
    // }

    // public bool RemoveContact(Guid contactId)
    // {
    //     CustomerContact? contact = Contacts.FirstOrDefault(c => c.Id == contactId);

    //     if(contact == null)
    //     {
    //         return false;
    //     }

    //     Contacts.Remove(contact);
    //     return true;
    // }

}