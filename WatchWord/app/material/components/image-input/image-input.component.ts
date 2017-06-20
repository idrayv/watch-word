import { Component, ElementRef, forwardRef, Output, ViewChild, OnInit } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor, Validator, NG_VALIDATORS, AbstractControl } from '@angular/forms';
import { MaterialService } from '../../material.service';

@Component({
    selector: 'image-input[mimeTypes]',
    templateUrl: 'app/material/components/image-input/image-input.template.html',
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

export class ImageInputComponent implements ControlValueAccessor, Validator, OnInit {
    private onChangeCallback: Function;
    private errors: Array<string> = [];
    private types: Array<string> = [];

    @ViewChild('file')
    fileInput: ElementRef;

    constructor(private materialService: MaterialService, private el: ElementRef) { }

    ngOnInit() {
        let attribute = this.el.nativeElement.attributes.getNamedItem('mimeTypes');
        this.types = (<string>attribute.value).split(';');
    };

    fileChanged(): void {
        let file = this.fileInput.nativeElement.files[0];
        if (file && this.types.some(t => t === file.type)) {
            this.callService(file);
        } else {
            this.addErrorsAndCleanInput([`Content type of this attachment is not allowed. Supported types: ${this.types.join(', ')}`]);
            this.onChangeCallback('');
        }
    }

    callService(file: File): void {
        let base64: string = '';
        this.materialService.parseImage(file).subscribe(
            response => {
                if (response.success) {
                    base64 = response.base64;
                    this.errors = [];

                } else {
                    this.addErrorsAndCleanInput(response.errors);
                }
                this.onChangeCallback(base64);
            },
            err => {
                this.addErrorsAndCleanInput(['Connection error']);
                this.onChangeCallback(base64);
            }
        );
    }

    addErrorsAndCleanInput(errors: Array<string>): void {
        this.errors = errors;
        this.fileInput.nativeElement.value = null;
    }

    registerOnChange(fn: any): void {
        this.onChangeCallback = fn;
    }

    validate(c: AbstractControl): { [key: string]: any; } {
        if (c.value && this.errors.length === 0) {
            return null;
        }
        return { 'imageInput': this.errors };
    }

    writeValue(image: string): void { }
    registerOnTouched(fn: any): void { }
    registerOnValidatorChange(fn: () => void): void { }
    setDisabledState(isDisabled: boolean): void { }
}