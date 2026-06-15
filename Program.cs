using MediatR;
using myTradingApp.Features.Test;
using myTradingApp.Common.Interface;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();
var endpointType = typeof(IEndPoint);
var assemblies = Assembly.GetExecutingAssembly();
var endpointClasses = assemblies.GetTypes()
    .Where(t => endpointType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

foreach (var type in endpointClasses)
{
    // Tạo instance và gọi phương thức MapEndpoints
    var instance = Activator.CreateInstance(type) as IEndPoint;
    instance?.MapEndPoints(app);
}
app.MapGet("/", () => "VSA Ready");

app.Run();
