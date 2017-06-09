import { Component, ElementRef, forwardRef, Output, ViewChild } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor, Validator, NG_VALIDATORS, AbstractControl } from '@angular/forms';
import { MaterialService } from "../../material/material.service";

@Component({
    selector: 'image-input',
    templateUrl: "app/components/image-input/image-input.template.html",
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => ImageInputComponent),
            multi: true
        },
        {
            provide: NG_VALIDATORS,
            useExisting: forwardRef(() => ImageInputComponent),
            multi: true
        }
    ]
})
export class ImageInputComponent implements ControlValueAccessor, Validator {
    private onChangeCallback: Function;
    private serverErrors: Array<string> = [];

    constructor(private materialService: MaterialService) { }

    @ViewChild('file')
    fileInput: ElementRef;

    writeValue(image: string): void {

    }

    fileChanged(): void {
        let file = this.fileInput.nativeElement.files[0];
        let base64: string = "";
        this.materialService.parseImage(file).subscribe(
            response => {
                if (response.success) {
                    base64 = "data:image/png;base64," + response.base64;
                    this.serverErrors = [];

                } else {
                    this.serverErrors = response.errors;
                }
                this.onChangeCallback(base64);
            },
            err => {
                this.serverErrors = new Array<string>("Connection error");
                this.onChangeCallback(base64);
            }
        );

    }

    registerOnChange(fn: any): void {
        this.onChangeCallback = fn;
    }

    registerOnTouched(fn: any): void {

    }

    setDisabledState(isDisabled: boolean): void {

    }

    validate(c: AbstractControl): { [key: string]: any; } {
        if (c.value && this.serverErrors.length === 0) {
            return null;
        }
        return { "imageInput": this.serverErrors };
    }

    registerOnValidatorChange(fn: () => void): void {

    }
}