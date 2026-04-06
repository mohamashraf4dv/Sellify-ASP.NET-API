namespace Sellify.Application.Features.Order.Command.CreateOrder
{
    public class CreateOrderValidator: AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleForEach(x => x.OrderDTO).NotEmpty().WithMessage("Invalid data sent from client")
                .ChildRules(order =>
                {
                    order.RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required");

                    order.RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
                    order.RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price cannot be less than 0");

                    order.RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantity is required");
                    order.RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity cannot be less than 0");
                });

            RuleForEach(x => x.ProductsIds).NotEmpty().WithMessage("Invalid data sent from Client")
                .ChildRules(productId=>
                {
                    productId.RuleFor(x => x).NotEmpty().WithMessage("ProductId is Required");
                });

        }
    }
}
