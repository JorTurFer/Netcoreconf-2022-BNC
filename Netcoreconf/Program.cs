using Netcoreconf.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Mount file provided by CSI Driver
builder.Configuration.AddKeyPerFile("/mnt/secrets-store", optional: true);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServiceBus(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
