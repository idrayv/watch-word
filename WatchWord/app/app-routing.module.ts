import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

export const routes: Routes = [
    { path: '', loadChildren: 'app/home/home.module#HomeModule' },
    { path: 'home', loadChildren: 'app/home/home.module#HomeModule' },
    { path: 'dictionaries', loadChildren: 'app/dictionaries/dictionaries.module#DictionariesModule' },
    { path: 'materials', loadChildren: 'app/materials/materials.module#MaterialsModule' },
    { path: 'create-material', loadChildren: 'app/create-material/create-material.module#CreateMaterialModule' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }