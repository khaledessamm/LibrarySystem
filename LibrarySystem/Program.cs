using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using LibrarySystem.Data;
using LibrarySystem.Service;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection") ??
           throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(Options => Options.UseSqlServer(connectionstring));


builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddFluentValidationAutoValidation()
      .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var mappingconfig = TypeAdapterConfig.GlobalSettings;
mappingconfig.Scan(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IMapper>(new Mapper(mappingconfig));



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
