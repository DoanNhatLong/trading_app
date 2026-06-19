using MediatR;
using myTradingApp.Data.Entity;

namespace myTradingApp.Features.PorfoliosBusiness;
public record CreateCommand(
    int UserId,
    int StockId,
    int Quantity
    ) : IRequest;
