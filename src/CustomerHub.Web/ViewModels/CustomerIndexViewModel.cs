public class CustomerIndexViewModel
{
    public string? Search { get; set; }
    public CustomerType? Type { get; set; }
    public CustomerStatus? Status { get; set; }
    public List<CustomerListViewModel> Customers { get; set; } = [];
    public int TotalCount { get; set; }
    public int FilteredCount { get; set; }
}