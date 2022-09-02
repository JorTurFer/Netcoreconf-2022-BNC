using Azure.Identity;
using Netcoreconf;
using Netcoreconf.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Mount file provided by CSI Driver
builder.Configuration.AddKeyPerFile("/mnt/secrets-store", optional: true);

// Register Azure Key Vault as a configuration source
if (!string.IsNullOrEmpty(builder.Configuration["KeyVaultName"]))
{
    builder.Configuration.AddAzureKeyVault(
        new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
        new DefaultAzureCredential(),
        new AzureKeyVaultSecretParser());
}

// Register ServiceBus client 
builder.Services.AddServiceBus(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
