import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialsComponent } from './materials.component'
import { MaterialsRoutingModule } from './materials-routing.module'

@NgModule({
    imports: [CommonModule, MaterialsRoutingModule],
    declarations: [MaterialsComponent],
    providers: []
})

export class MaterialsModule { }