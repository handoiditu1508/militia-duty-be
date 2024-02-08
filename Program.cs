using MilitiaDuty.Data;
using MilitiaDuty.Models.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MilitiaContext>();

builder.Services.Configure<MilitiaOptions>(builder.Configuration.GetRequiredSection(MilitiaOptions.Militia));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed database
var options = app.Configuration.GetRequiredSection(MilitiaOptions.Militia).Get<MilitiaOptions>();
if (options != null)
{
    await Seeder.Seed(options);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
