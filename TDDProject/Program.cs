using FluentValidation.AspNetCore;
using TDDProject.Interfaces;
using TDDProject.MongoDB;
using TDDProject.Services;
using TDDProject.Validators;
using static TDDProject.MongoDB.MongoDBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>()); builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
var settings = builder.Configuration.GetSection(nameof(MongoDBContext)).Get<MongoDbSettings>();
var _dbContext = new MongoDBContext(settings.ConnectionString, settings.DatabaseName, settings.CollectionName);
builder.Services.AddSingleton<MongoDBContext>(_dbContext);
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapHealthChecks(TDDProject.Constants.HealthCheckApi);

app.Run();
public partial class Program { }
