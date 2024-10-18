param location string = 'West Europe'
param applicationName string = 'FaceRecog'
param keyVaultName string = '${applicationName}-kv'
param signalRName string = '${applicationName}-sr'
param SqlServerName string = 'bachelor-gruppe1' //'${applicationName}-ss'
param dataBaseName string = 'Employees' //'${applicationName}-db'
param appServicePlanName string = '${applicationName}-asp'
param webSiteName string = '${applicationName}-wa'
param linuxFxVersion string = 'DOTNET|6.0'


var skuDefault = {
  name: 'B1'
  capacity: 1
}


resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' = {
  location: location
  name: keyVaultName
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: tenant().tenantId
    publicNetworkAccess: 'Enabled'
    enableSoftDelete: true
    enablePurgeProtection: true
    softDeleteRetentionInDays: 90
    enabledForDiskEncryption: false
    enabledForTemplateDeployment: true
    accessPolicies: []

  }
}

resource appServicePlan 'Microsoft.Web/serverfarms@2020-06-01' = {
  name: appServicePlanName
  location: location
  sku: skuDefault
  kind: 'windows'
}

resource appService 'Microsoft.Web/sites@2020-06-01' = {
  name: webSiteName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
  }
}
resource appServiceFrontEnd 'Microsoft.Web/sites@2020-06-01' = {
  name: signalRName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
  }
}

resource secretFaceServiceAPIKey 'Microsoft.KeyVault/vaults/secrets@2023-07-01' = {
  parent: keyVault
  name: 'FaceServiceAPIKey'
  properties: {
    value: '20cf2bd095bf4d7e8c1dd637102f1b9a'
  }
}

resource secretDatabasePassword 'Microsoft.KeyVault/vaults/secrets@2023-07-01' = {
  parent: keyVault
  name: 'DatabasePassword'
  properties: {
    value: '6UYLxQF9J&6q@63Po%BV'
  }
}

