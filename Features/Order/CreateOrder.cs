using MediatR;
using myTradingApp.Common.Interface;
using Microsoft.EntityFrameworkCore;
using myTradingApp.Data.Entity;

namespace myTradingApp.Features.OrderBusiness;

public record Query : IRequest<List<Order>>;

public class Handler(AppDbContext context) : IRequestHandler<CreateOrderCommand, int>
{
    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            UserId = request.UserId,
            StockId = request.StockId,
            OrderTypeId = request.OrderTypeId,
            Quantity = request.Quantity,
            Price = request.Price,
            OrderDate = DateTime.Now
        };
        context.Orders.Add(order);
        await context.SaveChangesAsync(cancellationToken);
        return order.Id;
    }
}

public class Endpoint : IEndPoint
{
    public void MapEndPoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/order", async (CreateOrderCommand command, IMediator mediator)
            =>
        {
            var orderId = await mediator.Send(command);
            return Results.Created($"/orders/{orderId}", orderId);
        });
    }
}
