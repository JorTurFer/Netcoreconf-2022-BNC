using Microsoft.AspNetCore.Mvc;

namespace Netcoreconf.Controllers
{
    public class DebugController: ControllerBase
    {
        private readonly IConfiguration _configuration;
        private const string ServiceAccountTokenFilePath = "/var/run/secrets/kubernetes.io/serviceaccount/token";
        private const string AzureTokenFilePath = "/var/run/secrets/azure/tokens/azure-identity-token";

        public DebugController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("environment")]
        public IActionResult GetEnvironmentVariables()
        {
            var environmentVariables = Environment.GetEnvironmentVariables();
            return Ok(environmentVariables);
        }

        [HttpGet("configuration")]
        public IActionResult GetConfiguration()
        {
            return Ok(_configuration.AsEnumerable());
        }

        [HttpGet("service-account-token")]
        public IActionResult GetServiceAccountToke()
        {
            var token = System.IO.File.Exists(ServiceAccountTokenFilePath)
                ? System.IO.File.ReadAllText(ServiceAccountTokenFilePath)
                : "";

            return Ok(token);
        }

        [HttpGet("azure-token")]
        public IActionResult GetAzureToken()
        {
            var token = System.IO.File.Exists(AzureTokenFilePath)
                ? System.IO.File.ReadAllText(AzureTokenFilePath)
                : "";

            return Ok(token);
        }
    }
}
