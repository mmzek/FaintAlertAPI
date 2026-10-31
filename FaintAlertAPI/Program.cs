using FaintAlertAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using FaintAlertAPI.Contracts;
using FaintAlertAPI.MappingProfiles;
using FaintAlertAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FaintAlertDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IEmergencyContactsService, EmergencyContactsService>();
builder.Services.AddAutoMapper(c => {c.AddProfile<EmergencyContactMappingProfile>(); 
    c.AddProfile<UserMappingProfile>(); 
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
