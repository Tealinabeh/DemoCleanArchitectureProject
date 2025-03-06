using DemoBookApp.Infrastructure;
using DemoBookApp.Application;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

builder.Services.AddInfrastructure(builder.Configuration)
                .AddApplication();

builder.Services.AddControllers();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();