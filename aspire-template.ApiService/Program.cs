using aspire.template.ApiService.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

builder.AddNpgsqlDbContext<ApplicationDbContext>("appdb");

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapDefaultEndpoints();

app.Run();