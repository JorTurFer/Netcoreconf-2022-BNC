using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;

namespace Netcoreconf
{
    public class AzureKeyVaultSecretParser : KeyVaultSecretManager
    {
        public override string GetKey(KeyVaultSecret secret)
            => secret.Name.Replace("--", ConfigurationPath.KeyDelimiter);
    }
}
