import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {WordComponent} from './word.component';
import {TranslationModalModule} from '../translation-modal/translation-modal.module';

@NgModule({
    imports: [CommonModule, TranslationModalModule],
    declarations: [WordComponent],
    exports: [WordComponent]
})
export class WordModule {
}
