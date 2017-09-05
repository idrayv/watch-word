import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { HomeRoutingModule } from './home-routing.module';
import { AbstractWordsComponent } from './components/abstract-words/abstract-words.component';
import { StatisticsService } from './statistics.service';
import { WordModule } from '../global/components/word/word.module';
import { ModalModule } from '../global/components/modal/modal.module';
import { TranslationModalModule } from '../global/components/translation-modal/translation-modal.module';

@NgModule({
    imports: [CommonModule, HomeRoutingModule, WordModule, ModalModule, TranslationModalModule],
    declarations: [HomeComponent, AbstractWordsComponent],
    providers: [StatisticsService]
})

export class HomeModule {
}