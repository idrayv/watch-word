import {NgModule} from '@angular/core';
import * as ApiServiceProxies from './service-proxies';

@NgModule({
    providers: [
        ApiServiceProxies.RoleServiceProxy,
        ApiServiceProxies.SessionServiceProxy,
        ApiServiceProxies.UserServiceProxy,
        ApiServiceProxies.TokenAuthServiceProxy,
        ApiServiceProxies.AccountServiceProxy,
        ApiServiceProxies.MaterialServiceProxy,
        ApiServiceProxies.TranslationServiceProxy,
        ApiServiceProxies.VocabularyServiceProxy,
        ApiServiceProxies.FavoriteMaterialServiceProxy
    ]
})
export class ServiceProxyModule {
}
