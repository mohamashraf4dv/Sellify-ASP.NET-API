namespace Sellify.Application.Features.Orders.Command.CreateOrder
{
    public class CreateOrderValidator: AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            //RuleForEach(x => x.OrderDTO).NotEmpty().WithMessage("Invalid data sent from client")
            //    .ChildRules(order =>
            //    {
            //        order.RuleFor(x => x.Key).NotEmpty().WithMessage("ProductId is required");

            //        order.RuleFor(x => x.Value.Price).NotEmpty().WithMessage("Price is required");
            //        order.RuleFor(x => x.Value.Price).GreaterThan(0).WithMessage("Price cannot be less than 0");

            //        order.RuleFor(x => x.Value.Quantity).NotEmpty().WithMessage("Quantity is required");
            //        order.RuleFor(x => x.Value.Quantity).GreaterThan(0).WithMessage("Quantity cannot be less than 0");
            //    });

            //RuleForEach(x => x.ProductsIds).NotEmpty().WithMessage("Invalid data sent from Client")
            //    .ChildRules(productId=>
            //    {
            //        productId.RuleFor(x => x).NotEmpty().WithMessage("ProductId is Required");
            //    });

        }
    }
}
