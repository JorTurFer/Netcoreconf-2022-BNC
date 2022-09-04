namespace Netcoreconf.ServiceBus
{
    public class ServiceBusOptions
    {
        public const string SectionName = "ServiceBus";
        public string Queue { get; set; } = null!;
        public string Connection { get; set; } = null!;
        public string Namespace { get; set; } = null!;
    }
}
