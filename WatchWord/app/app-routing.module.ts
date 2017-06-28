import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

export const routes: Routes = [
    { path: '', loadChildren: 'app/home/home.module#HomeModule' },
    { path: 'home', loadChildren: 'app/home/home.module#HomeModule' },
    { path: 'dictionaries', loadChildren: 'app/dictionaries/dictionaries.module#DictionariesModule' },
    { path: 'materials', loadChildren: 'app/materials/materials.module#MaterialsModule' },
    { path: 'material', loadChildren: 'app/material/material.module#MaterialModule' },
    { path: 'login', loadChildren: 'app/auth/login/login.module#LoginModule' },
    { path: 'register', loadChildren: 'app/auth/register/register.module#RegisterModule' },
    { path: 'settings', loadChildren: 'app/settings/settings.module#SettingsModule' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }