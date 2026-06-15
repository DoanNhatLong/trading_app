using System.Threading.Tasks;
using MediatR;
using myTradingApp.Common.Interface;

namespace myTradingApp.Features.Test;

public record GetTestQuery() : IRequest<string>;
public class GetTestHandle : IRequestHandler<GetTestQuery, string>
{
    public Task<string> Handle(GetTestQuery request, CancellationToken cancellationToken)
    => Task.FromResult("Hello world");

}

public class TestEndPoint : IEndPoint
{
    public void MapEndPoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/test", (IMediator mediator) => mediator.Send(new GetTestQuery()));
    }
}
