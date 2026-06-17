using MediatR;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using myTradingApp.Common.Interface;
using myTradingApp.Data.Entity;

namespace myTradingApp.Features.Auth;
public record LoginCommand(string Username, string Password)
  : IRequest<string>;

public class LoginHandler(AppDbContext context)
  : IRequestHandler<LoginCommand, string>
{
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(
            u => u.Username == request.Username, cancellationToken
            );
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Sai thông tin đăng nhập.");
        }
        return "fake-jwt-token";
    }
    public class LoginEndpoint : IEndPoint
    {
        public void MapEndPoints(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", (LoginCommand command, IMediator mediator)
                => mediator.Send(command).Result);
        }
    }
}
