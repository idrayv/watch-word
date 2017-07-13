import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DictionariesComponent } from './dictionaries.component'
import { DictionariesRoutingModule } from './dictionaries-routing.module'
import { DictionariesService } from './dictionaries.service';
import { TranslationModalModule } from '../global/components/translation-modal/translation-modal.module';
import { ModalModule } from '../global/components/modal/modal.module';
import { WordModule } from '../global/components/word/word.module';

@NgModule({
    imports: [CommonModule, DictionariesRoutingModule, ModalModule, TranslationModalModule, WordModule],
    declarations: [DictionariesComponent],
    providers: [DictionariesService]
})

export class DictionariesModule { }