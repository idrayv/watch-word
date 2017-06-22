import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MaterialsComponent } from './materials.component';
import { MaterialsRoutingModule } from './materials-routing.module';
import { PaginationComponent } from './pagination/pagination.component';
import { MaterialsService } from './materials.service';
import { PaginationService } from './pagination/pagination.service';

@NgModule({
    imports: [CommonModule, MaterialsRoutingModule, FormsModule],
    declarations: [MaterialsComponent, PaginationComponent],
    providers: [PaginationService, MaterialsService]
})

export class MaterialsModule { }