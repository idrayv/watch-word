import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MaterialsComponent } from './materials.component';

const routes: Routes = [{
  path: 'page/:id',
  component: MaterialsComponent
}, {
  path: '',
  component: MaterialsComponent,
  pathMatch: 'full'
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class MaterialsRoutingModule {
}
