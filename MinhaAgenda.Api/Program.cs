using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MinhaAgenda.Application.Apps;
using MinhaAgenda.Application.Apps.Interface;
using MinhaAgenda.Domain;
using MinhaAgenda.Domain.Validation;
using MinhaAgenda.Infrastructure.DbContexts;
using MinhaAgenda.Infrastructure.Exceptions.Handler;
using MinhaAgenda.Infrastructure.Repositories;
using MinhaAgenda.Infrastructure.Repositories.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPessoaApplication, PessoaApplication>();
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddTransient<IValidator<Pessoa>, PessoaValidator>();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
//builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("database"));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(x => x.AddDefaultPolicy(x => { x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); }));

//builder.Services.AddControllers().AddJsonOptions(x =>
//                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
