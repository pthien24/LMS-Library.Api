using LMS_Library.Api.Data.Models;
using LMS_Library.Service.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICourseManagment, CourseManagment>();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IDocumentManagement, DocumentManagement>();
// For Entity Framework
builder.Services.AddDbContext<DataBaseContext>(opts =>
{
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:Connection"]);
});
// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
