using MediatR;
using myTradingApp.Common.Interface;
using Microsoft.EntityFrameworkCore;
using myTradingApp.Data.Entity;

namespace myTradingApp.Features.User;

public record Query : IRequest<List<UserDto>>;

public class Handler(AppDbContext context) : IRequestHandler<Query, List<UserDto>>
{
    public Task<List<UserDto>> Handle(Query request, CancellationToken cancellationToken)
    {
        var users = context.Users
          .Select(u => new UserDto(
              u.Id,
              u.Username,
              u.Role.RoleName,
              u.CreatedAt
                ))
          .ToListAsync(cancellationToken);
        return users;
    }
}

public class Endpoint : IEndPoint
{
    public void MapEndPoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/user", (IMediator mediator)
            => mediator.Send(new Query()).Result);
    }
}
