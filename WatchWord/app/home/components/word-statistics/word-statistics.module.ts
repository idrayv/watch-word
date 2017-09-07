import { NgModule } from '@angular/core';
import { RouterModule } from "@angular/router";
import { CommonModule } from "@angular/common";
import { WordStatisticsService } from './word-statistics.service';
import { WordStatisticsComponent } from './word-statistics.component';
import { WordModule } from "../../../global/components/word/word.module";

@NgModule({
    imports: [RouterModule, CommonModule, WordModule],
    declarations: [WordStatisticsComponent],
    providers: [WordStatisticsService],
    exports: [WordStatisticsComponent]
})

export class WordStatisticsModule {
}