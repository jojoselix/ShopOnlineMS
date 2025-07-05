var builder = WebApplication.CreateBuilder(args);

// Add services to the continer

var app = builder.Build();

// Configure HTTP request pipeline

app.Run();
