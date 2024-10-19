using FluentValidation;

namespace PizzaWebAPI.DTOs.Validators
{
    public class PizzaDtoValidator : AbstractValidator<PizzaCreateDto>
    {
        public PizzaDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Pizza name is required")
                .MaximumLength(100).WithMessage("Pizza name can't be longer than 100 characters");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(p => p.Description)
                .MaximumLength(250).WithMessage("Description can't be longer than 250 characters");
        }
    }
}
