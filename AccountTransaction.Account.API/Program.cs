using AccountTransaction.Account.API.Configuration;
using AccountTransaction.Account.API.Configuration.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.RegisterServices();

var app = builder.Build();

DbMigrationHelpers.EnsureSeedData(app).Wait();

app.UseSwaggerConfiguration();

app.UseApiConfiguration(app.Environment);

app.Run();