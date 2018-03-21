# WatchWord

:zap: Angular project: ./angular  
:cloud: .Net core project: ./aspnet-core

# .Net Setup

00. Setup database connection string (MySql) in ./aspnet-core/src/WatchWord.Migrator/appsettings.json
01. Build & Run WatchWord.Migrator in order to migrate database
02. Build & Run WatchWord.Web.Host in order to run local API server

# Angular Setup

03. `cd ./angular`
04. `npm install` (Restore packages)
05. Setup remoteServiceBaseUrl in ./angular/src/assets/appconfig.json (Local or cloud API server)
06. `npm run hmr` (Run with hot module reload)

# Deploy

07. `cd ./angular`
08. `npm run deploy`
09. Publish ./aspnet-core/src/WatchWord.Web.Host/ to Azure
