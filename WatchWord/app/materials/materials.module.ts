import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialsComponent } from './materials.component';
import { MaterialsRoutingModule } from './materials-routing.module';
import { MaterialsService } from './materials.service';

@NgModule({
    imports: [CommonModule, MaterialsRoutingModule],
    declarations: [MaterialsComponent],
    providers: [MaterialsService]
})

export class MaterialsModule { }