import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TranslationModalComponent } from './translation-modal.component';
import { TranslationModalService } from './translation-modal.service';
import { TranslationService } from './translation.service';

@NgModule({
    imports: [FormsModule, CommonModule],
    declarations: [TranslationModalComponent],
    providers: [TranslationModalService, TranslationService],
    exports: [TranslationModalComponent]
})

export class TranslationModalModule { }