using AccountTransaction.WebUI.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityConfiguration();

builder.Services.AddMvcConfiguration(builder.Configuration);

builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

app.UseMvcConfiguration();

app.Run();