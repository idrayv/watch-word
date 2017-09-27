import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { HomeRoutingModule } from './home-routing.module';
import { ModalModule } from '../global/components/modal/modal.module';
import { TranslationModalModule } from '../global/components/translation-modal/translation-modal.module';
import { WordStatisticsModule } from './components/word-statistics/word-statistics.module';
import { MaterialStatisticsModule } from './components/material-statistics/material-statistics.module';

@NgModule({
    imports: [CommonModule, HomeRoutingModule, ModalModule, TranslationModalModule, WordStatisticsModule, MaterialStatisticsModule],
    declarations: [HomeComponent],
    providers: []
})

export class HomeModule {
}
