import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app.module';
import { enableProdMode } from '@angular/core';

let cfg = require('./config');
if (cfg.appConfig.isDebug == false) {
    enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule);