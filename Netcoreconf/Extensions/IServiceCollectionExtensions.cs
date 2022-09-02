using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

namespace Netcoreconf.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new ServiceBusOptions();
            var sbSection = configuration.GetSection(ServiceBusOptions.SectionName);
            sbSection.Bind(options);
            services.Configure<ServiceBusOptions>(sbSection);
            if (!string.IsNullOrEmpty(options.Connection))
            {
                services.AddAzureClients(builder =>
                {
                    builder.AddServiceBusClient(configuration.GetConnectionString(options.Connection));
                    builder.AddServiceBusAdministrationClient(configuration.GetConnectionString(options.Connection));
                });
            } else if (!string.IsNullOrEmpty(options.Namespace))
            {
                services.AddAzureClients(builder =>
                {
                    builder.AddServiceBusClientWithNamespace(options.Namespace)
                            .WithCredential(new DefaultAzureCredential());
                    builder.AddServiceBusAdministrationClientWithNamespace(options.Namespace)
                            .WithCredential(new DefaultAzureCredential());
                });
            }

            return services;
        }
    }
}
