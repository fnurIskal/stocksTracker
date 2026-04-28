using Microsoft.EntityFrameworkCore;
using stocksTracker.Data;
using stocksTracker.Interfaces;
using stocksTracker.Models;
using stocksTracker.Repository;
using stocksTracker.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IStockRepository,StockRepository>();

builder.Services.Configure<FinnhubSettings>(builder.Configuration.GetSection("FinnhubSettings"));

builder.Services.AddHttpClient<IFinnhubService, FinnhubService>();

builder.Services.AddScoped<IStockRepository, StockRepository>();


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
