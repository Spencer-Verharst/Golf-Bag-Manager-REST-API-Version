using Microsoft.EntityFrameworkCore;
using GolfBagManagerAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database context
builder.Services.AddDbContext<GolfBagDbContext>(options =>
    options.UseSqlite("Data Source=golfbag.db"));

// Register our services (scoped for database operations)
builder.Services.AddScoped<IGolfBag, DatabaseGolfBag>();
builder.Services.AddScoped<IClubFactory, ClubFactory>();

var app = builder.Build();

// Ensure database is created on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GolfBagDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();