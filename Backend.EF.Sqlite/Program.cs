using Backend.EF.Sqlite.Data;
using Backend.EF.Sqlite.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddRefitServices();

builder.Services.AddCors(
    options => options.AddPolicy("all", policy => policy.WithOrigins("*")
    .WithHeaders("*").WithMethods("*")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("all");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
