using MediatR;
using Microsoft.EntityFrameworkCore;
using myTradingApp.Common.Interface;
using myTradingApp.Data.Entity;

namespace myTradingApp.Features.Roles;

public record GetAllRolesQuery : IRequest<List<Role>>;

public class GetAll(AppDbContext context) : IRequestHandler<GetAllRolesQuery, List<Role>>
{
    public async Task<List<Role>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    => await context.Roles.ToListAsync(cancellationToken);

}

public class GetAllEP : IEndPoint
{
    public void MapEndPoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/role", (IMediator mediator)
            => mediator.Send(new GetAllRolesQuery()).Result);
    }
}
