# WatchWord

:zap: AngularJS project: /angular  
:cloud: .Net core web project: /aspnet-core

# .Net Setup

01. Setup ConnectionStrings in /aspnet-core/src/WatchWord.Migrator/appsettings.json
02. Build and Run WatchWord.Migrator in order to migrate database
03. Build and Run WatchWord.Web.Host in order to run local API server

# Angular Setup

04. cd ./angular
05. npm install (restore packages)
06. Setup remoteServiceBaseUrl in /angular/src/assets/appconfig.json (optional)
07. npm run hmr (run on localhost with hot module reload)

# Deploy

08. cd ./angular
09. npm run deploy
10. cd ./angular
11. Publish /aspnet-core/src/WatchWord.Web.Host/ to Azure

# Use

12. have fun with https://watchword.herokuapp.com/
