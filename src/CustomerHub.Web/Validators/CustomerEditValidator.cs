using FluentValidation;
public class CustomerEditValidator : AbstractValidator<CustomerEditViewModel>
{
    public CustomerEditValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

                RuleFor(x => x.Type).IsInEnum().WithMessage("Geçerli müşteri tipi seçiniz");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı zorunludur")
                            .MinimumLength(2).WithMessage("Ad en az 2 karakter olmak zorundadır")
                            .MaximumLength(200).WithMessage("Ad en fazla 200 karakter olmak zorundadır");

        RuleFor(x => x.TCKNOrVKN).NotEmpty().WithMessage("TCKN/VKN zorunludur")
                                 .Must(BeOnlyDigits).WithMessage("TCKN/VKN sadece rakam içermelidir");

        RuleFor(x => x.TCKNOrVKN).Length(11).WithMessage("TCKN 11 haneli olmalıdır").When(x => x.Type == CustomerType.Individual && !string.IsNullOrEmpty(x.TCKNOrVKN));
        RuleFor(x => x.TCKNOrVKN).Length(10).WithMessage("VKN 10 haneli olmalıdır").When(x => x.Type == CustomerType.Business && !string.IsNullOrEmpty(x.TCKNOrVKN));

        RuleFor(x => x)
            .Must(HaveEmailOrPhone)
            .WithMessage("Email veya Telefon alanlarından en az biri zorunludur");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Phone)
            .Matches(@"^[0-9\s\-\+\(\)]+$").WithMessage("Geçerli bir telefon numarası giriniz")
            .When(x => !string.IsNullOrWhiteSpace(x.Phone));

        RuleFor(x => x.AddressCity)
            .MaximumLength(100).WithMessage("Şehir en fazla 100 karakter olabilir");

        RuleFor(x => x.AddressLine)
            .MaximumLength(500).WithMessage("Adres en fazla 500 karakter olabilir");  
    }

    private bool BeOnlyDigits(string value)
    {
        if (string.IsNullOrEmpty(value))
            return true;

        return value.All(char.IsDigit);
    } 

    private bool HaveEmailOrPhone(CustomerEditViewModel model)
    {
        var hasEmail = !string.IsNullOrWhiteSpace(model.Email);
        var hasPhone = !string.IsNullOrWhiteSpace(model.Phone);
        return hasEmail || hasPhone;
    }
}