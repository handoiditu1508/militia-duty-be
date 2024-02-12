using MilitiaDuty.Data;
using MilitiaDuty.Models;
using MilitiaDuty.Models.Options;

var builder = WebApplication.CreateBuilder(args);

// CORS
string _appCors = "AppCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(_appCors, policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("X-Total-Count");
    });
});

// Add services to the container.
builder.Services.AddDbContext<MilitiaContext>();

builder.Services.Configure<MilitiaOptions>(builder.Configuration.GetRequiredSection(MilitiaOptions.Militia));

builder.Services.AddAutoMapper(typeof(MappingProfile));

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

app.UseCors(_appCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
