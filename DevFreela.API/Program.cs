using DevFreela.Application;
using DevFreela.Infrastructure;
using DevFreela.API.Filters;
using FluentValidation.AspNetCore;
using DevFreela.API.Validators;
using FluentValidation;
using System.Text.Json.Serialization;
using DevFreela.Infrastructure.Seeder;
using DevFreela.API;
using DevFreela.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<NewProjectInputModelValidator>();
builder.Services.AddControllers(opt => opt.Filters.Add(typeof(ConstraintValidatorFilter)))
    .AddJsonOptions(opt =>{
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings_DevFreelaCs");
ArgumentNullException.ThrowIfNull(connectionString);
builder.Services.AddDBContext(connectionString);
builder.Services.AddControllerMappers();
builder.Services.AddApplicationMappers();
builder.Services.AddCommands();
builder.Services.AddRepositories(connectionString);
builder.Services.AddServices();
builder.Services.AddSwaggerConfig();
builder.Services.AddAuthenticationConfig(builder);

var app = builder.Build();

DBSeeder.Seed(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevFreela.API v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapControllers();

app.Run();
