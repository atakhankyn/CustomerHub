using Microsoft.AspNetCore.Mvc;

public class CustomersController : Controller
{
    private readonly ICustomerRepository _repository;
    public CustomersController(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var customers = await _repository.GetAllAsync();
        var viewModels = customers.Select(c => new CustomerListViewModel
        {
            Id = c.Id,
            Name = c.Name,
            TypeText = GetTypeText(c.Type),
            StatusText = GetStatusText(c.Status),
            StatusBadgeClass = GetBadgeClass(c.Status),
            TCKNOrVKN = MaskTCKN(c.TCKNOrVKN),
        }).ToList();

        return View(viewModels);
    }

     public IActionResult Create()
    {
        return View();
    } 

    [HttpPost]
    public async Task<IActionResult> Create(CustomerCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var customer = ToEntity(model);
        await _repository.AddAsync(customer);
        return RedirectToAction(nameof(Index));


    }

    //Helper Methods

    private Customer ToEntity(CustomerCreateViewModel model)
    {
        return new Customer
        {
            Id = Guid.NewGuid(),
            Type = model.Type,
            Name = model.Name,
            TCKNOrVKN = model.TCKNOrVKN,
            AddressCity = model.AddressCity,
            AddressLine = model.AddressLine,
            Status = CustomerStatus.Active,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
    private string GetTypeText(CustomerType type)
    {
        switch (type)
        {
            case CustomerType.Individual:
                return "Bireysel";
            case CustomerType.Business:
                return "Kurumsal";
            default:
                return "Bilinmiyor";
        }     
    }

    private string GetStatusText(CustomerStatus status)
    {
        switch (status)
        {
            case CustomerStatus.Active:
                return "Aktif";
            case CustomerStatus.Suspended:
                return "Askıda";
            case CustomerStatus.Closed:
                return "Kapalı";
            default:
                return "Bilinmiyor";
        }
    }

    private string GetBadgeClass(CustomerStatus status)
    {
        switch (status)
        {
            case CustomerStatus.Active:
                return "bg-success";
            case CustomerStatus.Suspended:
                return "bg-warning";
            case CustomerStatus.Closed:
                return "bg-danger";
            default:
                return "bg-secondary";
        }
    }

    private string MaskTCKN(string tcknOrVkn)
    {
        if (string.IsNullOrEmpty(tcknOrVkn))
        {
            return string.Empty;
        }

        if (tcknOrVkn.Length <= 6)
        {
            return tcknOrVkn;
        }

        string first = tcknOrVkn.Substring(0,3);
        string last = tcknOrVkn.Substring(tcknOrVkn.Length-3);
        string mask = new string('*', tcknOrVkn.Length - 6);

        return $"{first}{mask}{last}";
    }
}