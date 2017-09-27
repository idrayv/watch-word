import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DictionariesComponent } from './dictionaries.component';

const routes: Routes = [{
    path: '',
    component: DictionariesComponent
}];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: []
})

export class DictionariesRoutingModule {
}
