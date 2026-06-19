using MediatR;
using Microsoft.EntityFrameworkCore;
using myTradingApp.Common.Interface;
using myTradingApp.Data.Entity;

namespace myTradingApp.Features.PorfoliosBusiness;

public record Query : IRequest<List<Portfolio>>;

public class GetAll(AppDbContext context) : IRequestHandler<Query, List<Portfolio>>
{
    public async Task<List<Portfolio>> Handle(Query request, CancellationToken cancellationToken)
    => await context.Portfolios.ToListAsync(cancellationToken);

}
public class Endpoint : IEndPoint
{
    public void MapEndPoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/port", (IMediator mediator)
            => mediator.Send(new Query()).Result);
    }
}
