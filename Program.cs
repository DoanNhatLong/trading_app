using MediatR;
using myTradingApp.Data.Entity;
using myTradingApp.Features.Test;
using myTradingApp.Common.Interface;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Đăng ký DbContext vào DI Container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

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
