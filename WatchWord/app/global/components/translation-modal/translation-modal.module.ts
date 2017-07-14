import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TranslationModalComponent } from './translation-modal.component';
import { TranslationModalService } from './translation-modal.service';
import { DictionariesService } from '../../../dictionaries/dictionaries.service';

@NgModule({
    imports: [FormsModule, CommonModule],
    declarations: [TranslationModalComponent],
    providers: [TranslationModalService, DictionariesService],
    exports: [TranslationModalComponent]
})

export class TranslationModalModule { }