import { Directive, forwardRef, Attribute, ElementRef } from '@angular/core';
import { Validator, AbstractControl, NG_VALIDATORS } from '@angular/forms';

@Directive({
    selector: '[fileType]',
    providers: [{
        provide: NG_VALIDATORS,
        useExisting: forwardRef(() =>
            FileTypeValidator), multi: true
    }]
})
export class FileTypeValidator implements Validator {
    private types: Array<string> = [];
    constructor(element: ElementRef) {
        let values: string = element.nativeElement.getAttribute('fileType');
        this.types = values.split(";");
    }
    validate(c: AbstractControl): { [key: string]: any; } {
        if (c.value && this.types.length > 0) {
            let valid: boolean = this.types.some((type) => type === c.value.type);
            if (!valid) {
                return { fileType: this.types };
            }
        }
        return null;
    }
    registerOnValidatorChange(fn: () => void): void {

    }
}