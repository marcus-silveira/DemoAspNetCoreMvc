using Demo.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddElmahConfiguration()
    .AddMvcConfiguration()
    .AddIdentityConfiguration()
    .AddIoCConfiguration();

var app = builder.Build();

app.UseMvcConfiguration();

app.Run();