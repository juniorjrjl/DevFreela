using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using DevFreela.API.Filters;
using FluentValidation.AspNetCore;
using DevFreela.API.Validators;
using FluentValidation;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<NewProjectInputModelValidator>();
builder.Services.AddControllers(opt => opt.Filters.Add(typeof(ConstraintValidatorFilter)));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings_DevFreelaCs");
builder.Services.AddDbContext<DevFreelaDbContext>(o=> o.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProjectCommand).Assembly));
builder.Services.AddScoped<IProjectQueryRepository, ProjectQueryRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISkillQueryRepository, SkillQueryRepository>();
builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

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
