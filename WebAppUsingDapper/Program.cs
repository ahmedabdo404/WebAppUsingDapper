using WebAppUsingDapper.Endpoints;
using WebAppUsingDapper.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    string connectionString = configuration.GetConnectionString("DefaultConnection")
        ?? throw new ApplicationException("connectionString is null");
    return new DbConnectionFactory(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapEmployeeEndpoint();
app.Run();
