using Sellify.Application.Features.Orders.Command.CreateOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sellify.Application.Features.Payment.Command.CreatePaymentSession
{
    public record CreatePaymentSessionRequest(IReadOnlyDictionary<Guid, CreatePaymentSessionUsingOrderDTO> Order, List<Guid> ProductsIds);

}
