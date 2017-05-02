import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'
import { CreateMaterialComponent } from './create-material.component'

const routes: Routes = [
    { path: '', component: CreateMaterialComponent }
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class CreateMaterialRoutingModule { }