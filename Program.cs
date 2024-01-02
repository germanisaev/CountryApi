using CRUDWebAPI.Data;
using CRUDWebAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddDbContext<DbContextClass>();

builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", builder => builder.WithOrigins(
        "http://localhost:4200",
        "https://localhost:7194"
    )
    .AllowAnyMethod()
    .AllowAnyHeader());
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


app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
