global using Microsoft.EntityFrameworkCore;
global using SmartSense.API.Base.BL;
global using SmartSense.API.Base.Data;
global using SmartSense.API.Base.DTO;
global using SmartSense.API.Base.Processes;
global using SmartSense.API.ServiceModule.BL;
global using SmartSense.API.ServiceModule.DTO;
global using SmartSense.API.ServiceModule.Services;
global using SmartSense.Database.Types;
global using SmartSense.Database.Types.XML;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped<IMessageLogger, FileMessageLogger>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPubService, PubService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<ISanitationWorkService, SanitationWorkService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();