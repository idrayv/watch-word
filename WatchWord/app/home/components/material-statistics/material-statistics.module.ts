import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MaterialStatisticsService } from './material-statistics.service';
import { MaterialStatisticsComponent } from './material-statistics.component';

@NgModule({
    imports: [RouterModule, CommonModule],
    declarations: [MaterialStatisticsComponent],
    providers: [MaterialStatisticsService],
    exports: [MaterialStatisticsComponent]
})

export class MaterialStatisticsModule {
}