// -----------------------------------------------------------------------------
// Specific Parameters
// -----------------------------------------------------------------------------
@description('Sql connection string.')
@secure()
param sqlConnection string

@description('Custom domain name for the front-end application.')
param frontEndCustomDomain string = ''

@secure()
@description('The app config endpoint. If not supplied, uri is built for the "shared" workload.')
param appConfigEndpoint string = ''

@description('The resource location.')
@minLength(3)
param location string = resourceGroup().location ?? ''

// -----------------------------------------------------------------------------
// Variables
// -----------------------------------------------------------------------------
@description('The environment prefix.')
var prefix = split(resourceGroup().name, '-')[0]

@description('The workload name.')
var workload = split(resourceGroup().name, '-')[2]

@description('The short location suffix.')
var locationShort = split(resourceGroup().name, '-')[3]

@description('Amalgam of the workload and the (short) location name.')
var suffix = length('${workload}-${locationShort}') < 3 ? 'xyz' : '${workload}-${locationShort}'

@description('Amalgam of the shared workload and the (short) location name.')
var sharedSuffix = 'lvshared-${locationShort}'

@description('The resource tags.')
var tags = resourceGroup().tags

@description('The final app config endpoint to use.')
var finalAppConfigEndpoint = empty(appConfigEndpoint) ? 'https://${prefix}-appconfig-${sharedSuffix}.azconfig.io' : appConfigEndpoint

@description('The name of shared resource group.')
var sharedRgName = '${prefix}-rg-${sharedSuffix}'

// -----------------------------------------------------------------------------
// Resources
// -----------------------------------------------------------------------------
module appInsightsDeploy 'br:devacrlvsharedweu.azurecr.io/bicep/modules/diagnostics/app-insights:v1' = {
  name: 'appInsightsDeploy'
  scope: resourceGroup(sharedRgName)
  params: {
    location: location
    prefix: prefix
    suffix: suffix
    tags: tags
  }
}

module appServicePlanDeploy 'br:devacrlvsharedweu.azurecr.io/bicep/modules/web/app-service-plan:v1' = {
  name: 'appServicePlanDeploy'
  params: {  
    location: location
    prefix: prefix
    suffix: suffix
    tags: tags   
  }
}

module appServiceDeploy 'br:devacrlvsharedweu.azurecr.io/bicep/modules/web/app-service:v1' = {
  name: 'appServiceDeploy'
  params: {
    appServicePlanId: appServicePlanDeploy.outputs.resourceId
    shortName: 'my'
    appSettings: [
      { name: 'ASPNETCORE_ENVIRONMENT', value: prefix }
    ]
    connectionStrings: [
      { name: 'AppConfig', value: finalAppConfigEndpoint }
      { name: 'AppInsights', value: appInsightsDeploy.outputs.connectionString }
      { name: 'SqlDb', value: sqlConnection }
    ]
    location: location
    prefix: prefix
    suffix: suffix
    tags: tags
  }
}

module staticWebAppDeploy 'br:devacrlvsharedweu.azurecr.io/bicep/modules/web/static-web-app:v1' = {
  name: 'staticWebAppDeploy'
  params: {
    customDomainName: frontEndCustomDomain
    shortName: ''
    location: location
    prefix: prefix
    suffix: suffix
    tags: tags
  }
}

// module appConfigDeploy_Open 'br:devacrlvsharedweu.azurecr.io/bicep/modules/integration/app-config:v1' = {
//   name: 'appConfigDeploy_Open'
//   scope: resourceGroup(sharedRgName)
//   params: {
//     disableLocalAccess: false // temporary; to make the following key updates work from scm..
//     prefix: prefix
//     suffix: suffix
//     location: location
//     tags: tags
//   }
// }

// module appConfigEntry1 'br:devacrlvsharedweu.azurecr.io/bicep/modules/integration/app-config-entry:v1' = {
//   name: 'appConfigEntry1'
//   scope: resourceGroup(sharedRgName)
//   dependsOn: [appConfigDeploy_Open]
//   params: {
//     appConfigResourceName: appConfigDeploy_Open.outputs.resourceName
//     name: 'Dynamic:Global:Apis:AboutMe'
//     value: appServiceDeploy.outputs.appUrl
//     label: prefix
//   }
// }

// module appConfigDeploy_Close 'br:devacrlvsharedweu.azurecr.io/bicep/modules/integration/app-config:v1' = {
//   name: 'appConfigDeploy_Close'
//   scope: resourceGroup(sharedRgName)
//   dependsOn: [appConfigEntry1]
//   params: {
//     disableLocalAccess: true // seal it back up
//     prefix: prefix
//     suffix: suffix
//     location: location
//     tags: tags
//   }
// }

// -----------------------------------------------------------------------------
// Role Assignments
// -----------------------------------------------------------------------------
module appServiceSharedAppConfigReaderDeploy 'br:devacrlvsharedweu.azurecr.io/bicep/modules/security/sp-assign-rg-role:v1' = {
  name: 'appServiceSharedAppConfigReaderDeploy'
  params: {
    role: 'App Configuration Data Reader'
    principalId: appServiceDeploy.outputs.resourcePrincipalId
    principalType: 'ServicePrincipal'
  }
}

// -----------------------------------------------------------------------------
// Output
// -----------------------------------------------------------------------------
output appServicePrincipalId string = appServiceDeploy.outputs.resourcePrincipalId
