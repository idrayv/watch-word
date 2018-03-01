import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

const routes: Routes = [
    {path: '', redirectTo: '/app/materials', pathMatch: 'full'},
    {
        path: 'account',
        loadChildren: 'account/account.module#AccountModule',
        data: {preload: true}
    },
    {
        path: 'app',
        loadChildren: 'app/app.module#AppModule',
        data: {preload: true}
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    providers: []
})
export class RootRoutingModule {

}
