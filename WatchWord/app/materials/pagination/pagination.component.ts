import { Component, OnInit, forwardRef } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';
import { PaginationService } from './pagination.service';
import { MaterialsService } from '../materials.service';
import { PaginationServiceModel, PaginationModel } from './pagination.models';

@Component({
    selector: 'pagination',
    templateUrl: 'app/materials/pagination/pagination.template.html',
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => PaginationComponent),
            multi: true
        }
    ]
})

export class PaginationComponent implements OnInit, ControlValueAccessor {
    private model: PaginationServiceModel;

    constructor(private pagination: PaginationService) { }

    writeValue(model: PaginationModel): void {
        if (model) {
            this.model = this.pagination.getPaginator(model);
        }
    }

    registerOnChange(fn: any): void { }

    registerOnTouched(fn: any): void { }

    setDisabledState(isDisabled: boolean): void { }

    ngOnInit(): void {
    }
}