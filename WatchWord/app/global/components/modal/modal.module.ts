import { NgModule } from '@angular/core';
import { ModalService } from './modal.service';
import { ModalComponent } from './modal.component';

@NgModule({
    imports: [],
    declarations: [ModalComponent],
    providers: [ModalService],
    exports: [ModalComponent]
})

export class ModalModule { }