import { Component, ElementRef, OnInit } from '@angular/core';
import { BaseComponent } from '../../../global/base-component';
import { MaterialStatisticsService } from './material-statistics.service';
import { MaterialStatisticsResponseModel } from './material-statistics.models';
import { ControlValueAccessor } from '@angular/forms/src/forms';
import { Material, VocabWord } from '../../../material/material.models';

@Component({
    selector: 'material-statistics',
    templateUrl: 'material-statistics.template.html',
})
export class MaterialStatisticsComponent extends BaseComponent implements OnInit {
    private materials: Material[];

    constructor(private el: ElementRef, private statisticsService: MaterialStatisticsService) {
        super();
    }

    ngOnInit(): void {
        let attribute = this.el.nativeElement.attributes.getNamedItem('url');
        var url = <string>attribute.value;
        this.statisticsService.getRandomMaterials(url).then(response => this.fillModel(response));
    }

    private fillModel(response: MaterialStatisticsResponseModel): void {
        if (response.success) {
            this.materials = response.materials;
        } else {
            this.displayErrors(response.errors);
        }
    }
}