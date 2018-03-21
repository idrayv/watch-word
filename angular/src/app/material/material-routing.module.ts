import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {MaterialComponent} from './material.component';

const routes: Routes = [
    {
        path: ':id',
        component: MaterialComponent
    }, {
        path: 'create',
        component: MaterialComponent
    },
    {
        path: '',
        redirectTo: 'create',
        pathMatch: 'full'
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class MaterialRoutingModule {
}
