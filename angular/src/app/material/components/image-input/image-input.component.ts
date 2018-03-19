import {Component, ElementRef, forwardRef, ViewChild, OnInit} from '@angular/core';
import {NG_VALUE_ACCESSOR, ControlValueAccessor, Validator, NG_VALIDATORS, AbstractControl} from '@angular/forms';
import {MaterialService} from '../../material.service';

@Component({
    selector: 'ww-image-input[mimeTypes]',
    templateUrl: 'image-input.template.html',
    providers: [{
        provide: NG_VALUE_ACCESSOR,
        useExisting: forwardRef(() => ImageInputComponent),
        multi: true
    }, {
        provide: NG_VALIDATORS,
        useExisting: forwardRef(() => ImageInputComponent),
        multi: true
    }]
})
export class ImageInputComponent implements ControlValueAccessor, Validator, OnInit {

    private onChangeCallback: Function;
    private error: string[] = [];
    private types: string[] = [];

    @ViewChild('file') fileInput: ElementRef;

    constructor(private materialService: MaterialService, private el: ElementRef) {
    }

    ngOnInit() {
        const attribute = this.el.nativeElement.attributes.getNamedItem('mimeTypes');
        this.types = (<string>attribute.value).split(';');
    }

    fileChanged(): void {
        const file = this.fileInput.nativeElement.files[0];
        if (file && this.types.some(t => t === file.type)) {
            this.callService(file);
        } else {
            this.addErrorsAndCleanInput(
                [`Content type of this attachment is not allowed. Supported types: ${this.types.join(', ')}`]);
            this.onChangeCallback('');
        }
    }

    callService(file: File): void {
        let base64 = '';
        abp.ui.setBusy();
        this.materialService.parseImage(file).then(response => {
            abp.ui.clearBusy();
            if (response.success) {
                base64 = response.base64;
                this.error = [];

            } else if (this.error) {
                this.addErrorsAndCleanInput(response.error);
            } else {
                this.addErrorsAndCleanInput(['Server unavailable.']);
            }
            this.onChangeCallback(response.result);
        });
    }

    addErrorsAndCleanInput(error: string[]): void {
        this.error = error;
        this.fileInput.nativeElement.value = null;
    }

    registerOnChange(fn: any): void {
        this.onChangeCallback = fn;
    }

    validate(c: AbstractControl): { [key: string]: any; } {
        if (c.value && this.error.length === 0) {
            return null;
        }
        return {'imageInput': this.error};
    }

    writeValue(image: string): void {
    }

    registerOnTouched(fn: any): void {
    }

    registerOnValidatorChange(fn: () => void): void {
    }

    setDisabledState(isDisabled: boolean): void {
    }
}
