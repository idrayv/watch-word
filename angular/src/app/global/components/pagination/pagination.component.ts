import {Component, forwardRef} from '@angular/core';
import {NG_VALUE_ACCESSOR, ControlValueAccessor} from '@angular/forms';
import {PaginationServiceModel, PaginationModel} from './pagination.models';
import {PaginationHelperService} from './pagination-helper.service';

@Component({
    selector: 'ww-pagination',
    templateUrl: 'pagination.template.html',
    providers: [{
        provide: NG_VALUE_ACCESSOR,
        useExisting: forwardRef(() => PaginationComponent),
        multi: true
    }]
})
export class PaginationComponent implements ControlValueAccessor {

    public model: PaginationServiceModel;

    constructor(private pagination: PaginationHelperService) {
    }

    writeValue(model: PaginationModel): void {
        if (model) {
            this.model = this.pagination.getPaginator(model);
        }
    }

    registerOnChange(fn: any): void {
    }

    registerOnTouched(fn: any): void {
    }

    setDisabledState(isDisabled: boolean): void {
    }
}
