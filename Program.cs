using Microsoft.EntityFrameworkCore;
using GolfBagManagerAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GolfBagDbContext>(options =>
    options.UseSqlite("Data Source=golfbag.db"));

builder.Services.AddScoped<IGolfBag, DatabaseGolfBag>();
builder.Services.AddScoped<IClubFactory, ClubFactory>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GolfBagDbContext>();
    db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();


app.Run();
