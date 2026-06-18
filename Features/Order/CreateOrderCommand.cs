using MediatR;
using myTradingApp.Data.Entity;

namespace myTradingApp.Features.OrderBusiness;
public record CreateOrderCommand(
    int UserId,
    int StockId,
    int OrderTypeId,
    int Quantity,
    decimal Price
    ) : IRequest<int>;
