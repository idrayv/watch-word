# WatchWord

AngularJS project: \WatchWord\WatchWord\src
.Net core web project: \WatchWord\WatchWord\WatchWord.csproj

# Setup

01. cd \WatchWord\WatchWord\src
02. npm install (restore packages)
03. npm install --global webpack  (install webpack globally, do it once)

# Development

04. Setup api route in \WatchWord\src\environments\environment.ts as 'http://watchword.azurewebsites.net/api' (cloud back-end API)
05. npm run start (run on localhost)

# Deploy

06. npm run build
07. Publish WatchWord.csproj to Azure

# Back-end development:

01. Setup connection string in \WatchWord\appsettings.json
02. Configure auto picking of CS in WatchWord\WatchWord.DataAccess\DatabaseSettings.cs (optional)
03. Run WatchWord web project
04. Setup api route in \WatchWord\src\environments\environment.ts as 'http://localhost:54093/api' (local beck-end API)
05. npm run start (run on localhost)
