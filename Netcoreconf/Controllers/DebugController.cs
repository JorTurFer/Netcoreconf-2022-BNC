using Microsoft.AspNetCore.Mvc;


namespace Netcoreconf.Controllers
{
    public class DebugController: ControllerBase
    {
        private readonly IConfiguration _configuration;
        private const string serviceAccountTokenFile = "/var/run/secrets/kubernetes.io/serviceaccount/token";
        private const string azureTokenFile = "/var/run/secrets/azure/tokens/azure-identity-token";


        public DebugController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("environment")]
        public IActionResult GetEnvirnmentVariables()
        {
            var envirnmentVariables = Environment.GetEnvironmentVariables();
            return Ok(envirnmentVariables);
        }

        [HttpGet("configuration")]
        public IActionResult GetConfiguration()
        {
            return Ok(_configuration.AsEnumerable());
        }

        [HttpGet("service-account-token")]
        public IActionResult GetServiceAccountToke()
        {
            var token = "";
            if (System.IO.File.Exists(serviceAccountTokenFile))
            {
                token = System.IO.File.ReadAllText(serviceAccountTokenFile);
            }
            return Ok(token);
        }

        [HttpGet("azure-tokent")]
        public IActionResult GetAzureToken()
        {
            var token = "";
            if (System.IO.File.Exists(azureTokenFile))
            {
                token = System.IO.File.ReadAllText(azureTokenFile);
            }
            return Ok(token);
        }
    }
}
