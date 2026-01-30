using Microsoft.AspNetCore.Mvc;
using FluentValidation;

public class CustomersController : Controller
{
    private readonly ICustomerRepository _repository;
    private readonly IValidator<CustomerCreateViewModel> _createValidator;
    private readonly IValidator<CustomerEditViewModel> _editValidator;

    public CustomersController(
        ICustomerRepository repository,
        IValidator<CustomerCreateViewModel> createValidator,
        IValidator<CustomerEditViewModel> editValidator)
    {
        _repository = repository;
        _createValidator = createValidator;
        _editValidator = editValidator;
    }

    public async Task<IActionResult> Index()
    {
        var customers = await _repository.GetAllAsync();
        var viewModels = customers.Select(c => new CustomerListViewModel
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email,
            Phone = c.Phone,
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
        var validationResult = await _createValidator.ValidateAsync(model);
        
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return View(model);
        }

        var customer = ToEntity(model);
        await _repository.AddAsync(customer);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var customer = await _repository.GetByIdAsync(id);

        if(customer == null)
        {
            return NotFound();
        }

        var viewModel = ToDetails(customer);
        return View(viewModel);

    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var customer = await _repository.GetByIdAsync(id);

        if(customer == null)
        {
            return NotFound();
        }

        if(!customer.CanBeEdited())
        {
            return RedirectToAction(nameof(Details), new {id});
        }

        var viewModel = ToEdit(customer);
        return View(viewModel);

    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, CustomerEditViewModel model)
    {
        var validationResult = await _editValidator.ValidateAsync(model);
        
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return View(model);
        }

        var customer = await _repository.GetByIdAsync(id);

        if(customer == null)
        {
            return NotFound();
        }

        if(!customer.CanBeEdited())
        {
            return RedirectToAction(nameof(Details), new {id});
        }

        UpdateEntity(customer, model);
        await _repository.UpdateAsync(customer);

        return RedirectToAction(nameof(Details), new { id });        

    }

    //Helper Methods

    private void UpdateEntity(Customer customer,  CustomerEditViewModel model)
    {
        customer.Type = model.Type!.Value;
        customer.Name = model.Name;
        customer.TCKNOrVKN = model.TCKNOrVKN;
        customer.AddressCity = model.AddressCity;
        customer.AddressLine = model.AddressLine;
        customer.Email = model.Email;
        customer.Phone = model.Phone;
        customer.UpdatedAt = DateTime.UtcNow;     
    }

    private CustomerEditViewModel ToEdit(Customer customer)
    {
        return new CustomerEditViewModel
        {
        Id = customer.Id,
        Name = customer.Name,
        Type = customer.Type,
        TypeText = GetTypeText(customer.Type),
        Status = customer.Status,
        StatusText = GetStatusText(customer.Status),
        TCKNOrVKN = customer.TCKNOrVKN,
        AddressCity = customer.AddressCity,
        AddressLine = customer.AddressLine,
        Email = customer.Email,
        Phone = customer.Phone         
        };
    }

    private CustomerDetailsViewModel ToDetails(Customer customer)
    {
        return new CustomerDetailsViewModel
        {
        Id = customer.Id,
        Name = customer.Name,
        Type = customer.Type,
        TypeText = GetTypeText(customer.Type),
        Status = customer.Status,
        StatusText = GetStatusText(customer.Status),
        StatusBadgeClass = GetBadgeClass(customer.Status),
        TCKNOrVKN = customer.TCKNOrVKN,
        AddressCity = customer.AddressCity,
        AddressLine = customer.AddressLine,
        CreatedAt = customer.CreatedAt,
        UpdatedAt = customer.UpdatedAt,
        CanBeEdited = customer.CanBeEdited(),
        Email = customer.Email,
        Phone = customer.Phone,            
        };
    }

    private Customer ToEntity(CustomerCreateViewModel model)
    {
        return new Customer
        {
            Id = Guid.NewGuid(),
            Type = model.Type!.Value,
            Name = model.Name,
            Email = model.Email,
            Phone = model.Phone,
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