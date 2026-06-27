using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.RateLimiting;
using RentCarServer.Application;
using RentCarServer.Infrastructure;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddRateLimiter(cfr=>
{
    cfr.AddFixedWindowLimiter("fixed", options =>
    {
        options.PermitLimit = 10;
        options.Window = TimeSpan.FromSeconds(1);
        options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 100;
    });
});
builder.Services
    .AddControllers()
    .AddOData(options =>
    {
        options.Select().Filter().Count().Expand().OrderBy().SetMaxTop(null);
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddOpenApi();
var app = builder.Build();
app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

app.UseCors(x => x
   .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader()
   .SetPreflightMaxAge(TimeSpan.FromMinutes(10)) // Optional: Set the max age for preflight requests it can help reduce the number of preflight requests made by the browser
);

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireRateLimiting("fixed");

app.MapGet("/", () => "Hello World!");

app.Run();
