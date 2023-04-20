using EMixCepFinder.API.Extensions;
using EMixCepFinder.Domain.Service;
using EMixCepFinder.Infrastructure.Database.Extensions;
using EmixCepFinder.Service;
using EMixCepFinder.Infrastructure.Repository;
using EMixCepFinder.Domain.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLocalDb(builder.Configuration);

//DI internal services
builder.Services.AddScoped<ICepFinderService, CepFinderService>();
builder.Services.AddScoped<ICepFinderRepository, CepFinderRepository>();

// Add external services
builder.Services.AddViaCepService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
