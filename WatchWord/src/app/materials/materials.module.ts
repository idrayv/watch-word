import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MaterialsComponent } from './materials.component';
import { MaterialsRoutingModule } from './materials-routing.module';
import { PaginationComponent } from '../global/components/pagination/pagination.component';
import { PaginationHelperService } from '../global/components/pagination/pagination-helper.service';
import { MaterialsPaginationService } from './materials-pagination.service';

@NgModule({
    imports: [CommonModule, MaterialsRoutingModule, FormsModule],
    declarations: [MaterialsComponent, PaginationComponent],
    providers: [PaginationHelperService, MaterialsPaginationService]
})

export class MaterialsModule {
}
