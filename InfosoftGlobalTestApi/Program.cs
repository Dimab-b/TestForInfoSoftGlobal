using Dapper;
using InfosoftGlobalTestApi.Api.Middlewares;
using InfosoftGlobalTestApi.Application.Common.Interfaces;
using InfosoftGlobalTestApi.Domain.AuditLogs;
using InfosoftGlobalTestApi.Domain.Breeders;
using InfosoftGlobalTestApi.Domain.Common;
using InfosoftGlobalTestApi.Domain.Litters;
using InfosoftGlobalTestApi.Infrastructure.AuditLogs.Services;
using InfosoftGlobalTestApi.Infrastructure.Breeders.Services;
using InfosoftGlobalTestApi.Infrastructure.Common;
using InfosoftGlobalTestApi.Infrastructure.Common.Services;
using InfosoftGlobalTestApi.Infrastructure.Litters.Services;
using InfosoftGlobalTestApi.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
SqlMapper.AddTypeHandler(new GuidTypeHandler());
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>());
builder.Services.AddScoped<IBreederRepository, BreederRepository>();
builder.Services.AddScoped<ILitterRepository, LitterRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<INotificationService, ConsoleNotificationService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(InfosoftGlobalTestApi.Application.Litters.Commands.LitterPublishCommand).Assembly));
builder.Services.AddExceptionHandler<GlobalExceptionMiddleware>();
builder.Services.AddProblemDetails();
var app = builder.Build();
app.UseExceptionHandler();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
