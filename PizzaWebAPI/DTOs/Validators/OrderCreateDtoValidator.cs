using FluentValidation;

namespace PizzaWebAPI.DTOs.Validators
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(o => o.PizzaId)
                .GreaterThan(0).WithMessage("PizzaId must be greater than 0");

            RuleFor(o => o.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}
