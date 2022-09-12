# Netcoreconf-2022-BNC

This is a project to explore the different ways we have to use secrets in .NET apps running on Kubernetes.

## Getting Started

### Prerequisites

* AKS cluster deployed in Azure

### Installation

- Azure AD Workload identity: follow the [installation guide](https://azure.github.io/azure-workload-identity/docs/installation.html) and [quickstart](https://azure.github.io/azure-workload-identity/docs/quick-start.html).
- Secrets Store CSI Driver: follow the [installation guide](https://secrets-store-csi-driver.sigs.k8s.io/getting-started/installation.html).
- Apply all files in the `deploy folder`
  - `kubectl apply -f deploy`
  
## Demo

### [00-never-api.yaml](https://github.com/JorTurFer/Netcoreconf-2022-BNC/tree/main/deploy/00-never-api.yaml)

We deploy a .NET API using secrets as environment variables in the  Deployment definition. You should never use this approach.

### [01-secret-api.yaml](https://github.com/JorTurFer/Netcoreconf-2022-BNC/tree/main/deploy/01-secret-api.yaml)

The secret is deployed now as a kubernetes Secret (Opaque) and mounted as environment variable in the container.

### [02-csi-api.yaml](https://github.com/JorTurFer/Netcoreconf-2022-BNC/tree/main/deploy/02-csi-api.yaml)

We use here the Secrets Store CSI Driver approach using a SecretProviderClass and mounting the secrets in a volume.

### [03-key-vault-api.yaml](https://github.com/JorTurFer/Netcoreconf-2022-BNC/tree/main/deploy/03-key-vault-api.yaml)

We get the secrets from the KeyVault at application level. We don't have any secrets mounted in the container.

### [04-wi-api.yaml](https://github.com/JorTurFer/Netcoreconf-2022-BNC/tree/main/deploy/04-wi-api.yaml)

The last scenario is the Azure AD Workload Identity. With this approach, we use the native OIDC federation to connect with the Azure resources without secrets. The only secret mounted in the POD is the JWT from the service account to get the AD token to make calls to Azure resources.

## References

* [Secrets Store CSI Driver](https://secrets-store-csi-driver.sigs.k8s.io/)
* [Azure AD Workload Identity](https://azure.github.io/azure-workload-identity/docs/)
* [Kubernetes Secrets](https://kubernetes.io/docs/concepts/configuration/secret/)
* [GitHub Actions Workload Identity](https://github.com/MicrosoftDocs/azure-docs/blob/main/articles/active-directory/develop/workload-identity-federation-create-trust.md#github-actions)
