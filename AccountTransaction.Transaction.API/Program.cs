using AccountTransaction.Transaction.API.Configuration;
using AccountTransaction.Commom.Core.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddOpenTelemetry(builder.Configuration);

var app = builder.Build();

DbMigrationHelpers.EnsureSeedData(app).Wait();

app.UseSwaggerConfiguration();

app.UseApiConfiguration(app.Environment);

app.Run();