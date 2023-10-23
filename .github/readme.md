# GitHub Repo Setup
## Steps
  1. Generate a new SSH Key Pair
  1. Add a Deploy Key called `COMMIT_KEY_PUB` containing the public part of the SSH key (do give write access)
  1. Add a Secret called `COMMIT_KEY` containing the private part of the SSH key
  1. Set Workflow permissions to: 'Read and write permissions' (Settings > Actions > General)
  1. Add a Secret called `READ_REPO_PACKAGES` containing the *packages:read* central PAT Token
  1. Change every reference to DemoLibrary! **NB:** *Especially the "repo url" in the csproj file(s) :')*

## Justification
### VersionPrefix
Currently the csproj must have `<VersionPrefix>` set. Initially, this can be 0.0.1, or 1.0.0, etc. It serves as the working pre-release. 
Pushing a tag of a stable release version (e.g. v1.2.3) causes the package to be pushed with the tag version. The `<VersionPrefix>` is then incremented in the patch - in this case 1.2.4.  Pushing pre-release version tags (e.g. v1.2.3-pre.02) is done in CI. A package is pushed, but no changes are made to the source csproj. Manually pushing pre-release tags (or indeed creating in GitHub via Releases) is not well-supported. 

### Deploy Keys
In order for the `tag-ci` pipeline's "tag push" to trigger the `publish` pipeline, the default GITHUB_TOKEN cannot be used. (Workflows are prevented from triggering other ones using this default token).

Using a PAT (personal access token) to achieve the above is not appropriate in organisational settings, as it requires a dedicated user and its scope is not limited to just the repository thus presenting an unnecessary security concern...

... Say hello to Deploy Keys! Using these, one can easily trigger other workflows from a workflow. To use them, follow these steps:
1. Generate an ed25519 ssh key (anywhere you like!):
    ```bash
    ssh-keygen -t ed25519 -f id_ed25519 -N "" -q -C ""
    cat id_ed25519.pub id_ed25519
    ```
1. In GitHub repo, add a new Deploy Key (in Settings), providing just the **public** key. Name it `COMMIT_KEY_PUB`, for example
1. In GitHub repo, add a new repository secret (Settings), providing the **private** key. Name it `COMMIT_KEY`, for example
1. In your workflow, just make sure you check out using the ssh-key (this overrides the default GITHUB_TOKEN):
    ```yml    
    - uses: actions/checkout@v3
      with:
        ssh-key: "${{ secrets.COMMIT_KEY }}"
    ```
1. And now, any branch/tag pushes, etc will not be ignored in otherwise-passing workflow filters

### Workflow read and write permissions
Reasons for this are two-fold: 
- Allows for updating version numbers in source code where needed
- Enables the "dawidd6/action-download-artifact@v2" mechanism to work (the per-environment deployment_map)

### Read Repo Packages
In order to pull packages from other private repos (in this account), a limited PAT token is used.
This PAT token is defined centrally and has just package-read access (to all repos). This has already been defined, but is:

`Account > Settings > Developer Settings > Personal Access Tokens`
- add one with read:packages

Any repo that references packages from github-based nuget feeds will need auth to read packages. The PAT can be added as a secret in each repo requiring such access. Add a secret called `READ_REPO_PACKAGES` and pop the PAT in it :)

Additionally, to re-use the PAT for local workstation development (to pull in the packages in Visual Studio NuGet Package Manager, for example) add the following nuget config:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    ...
    <add key="ne1410s" value="https://nuget.pkg.github.com/ne1410s/index.json" />
  </packageSources>
  <packageSourceCredentials>
    <ne1410s>
      <add key="Username" value="ne1410s" />
      <add key="ClearTextPassword" value="YOUR_PAT_HERE" />
    </ne1410s>
  </packageSourceCredentials>
</configuration>
```

## Initial Setup

### Run this script
```powershell

$subId=az account list --all --query "[?starts_with(name, 'Main')].{id:id}" -o tsv; `
az account set -s $subId; `

$cloudenv="dev"; `
$workload="about"; `
$location="westeurope"; `
$locationShort="weu"; `
$sharedWorkload="lvshared"; `

$scmSpName="ghscm_$workload"; `
$scmSpId=az ad sp list --disp $scmSpName --only-show-errors --query '[].id' -o tsv; `
$scmCreds=""; `
if ( !$scmSpId ) { `
  $credsMap="{clientId:appId, clientSecret:password, subscriptionId: '$subId', tenantId:tenant}"; `
  $scmCreds=az ad sp create-for-rbac --name $scmSpName --only-show-errors --query $credsMap; `
  $scmSpId=az ad sp list --disp $scmSpName --only-show-errors --query '[].id' -o tsv; `
} `
$rgName="$cloudenv-rg-$workload-$locationShort"; `
$sharedRgName="$cloudenv-rg-$sharedWorkload-$locationShort"; `
$rgId=az group create -l $location -n $rgName --tags workload=$workload env=$cloudenv --query id -o tsv; `
$sharedRgId=az group show -g $sharedRgName --query id -o tsv; `
$scmRgOwner=az role assignment create --assignee-object-id $scmSpId --assignee-principal-type ServicePrincipal --role owner --scope $rgId --only-show-errors; `
$scmSharedRgOwner=az role assignment create --assignee-object-id $scmSpId --assignee-principal-type ServicePrincipal --role owner --scope $sharedRgId --only-show-errors; `

$engAdId=az ad group list --display-name engineers --query '[].id' -o tsv; `
$adminsAdId=az ad group list --display-name admins --query '[].id' -o tsv; `

clear; `
echo "--------------------------------------------------------------------------------"; `
echo "AZURE_WORKLOAD:   $workload"; `
echo "AZURE_RG_LOC:     $locationShort"; `
echo "AZURE_ENG_ID:     $engAdId"; `
echo "AZURE_ADMINS_ID:  $adminsAdId"; `
echo "AZURE_SUB_ID:     $subId"; `
echo "AZURE_SCM_ID:     $scmSpId"; `
echo "AZURE_SQL_CONN:   <SUPPLY SMTH>"; `
echo "AZURE_CI_PREFIX:  <e.g. dev>"; `
echo "API_BASE_URL:     <ADD SMTH>"; `
if ( $scmCreds ) { echo "AZURE_SCM_CREDS:" $scmCreds; } `
echo "--------------------------------------------------------------------------------"; `
 
```

## To connect to SQL via a managed identity, need to run:

```sql
-- NB: Must sign in with AAD, and switch to appropriate db first!
CREATE USER [<identity-name>] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [<identity-name>];
ALTER ROLE db_datawriter ADD MEMBER [<identity-name>];
-- The following is necessary for modifying schema (e.g. SCM SP / engineers running ef migrations bundle)
--GRANT ALTER ON Schema :: DBO TO engineers
--GRANT CREATE TABLE TO engineers
```

*The <identity-name> can be the name of the Azure App Service (e.g. direct managed identity),
or alternatively the display name of an AAD group, if that's how you prefer to roll..

