import { Component, ElementRef, forwardRef, Output, ViewChild } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';

@Component({
    selector: 'file-input',
    templateUrl: "app/components/file-input/file-input.template.html",
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => FileInputComponent),
            multi: true
        }
    ]
})
export class FileInputComponent implements ControlValueAccessor {
    private onChangeCallback: Function;
    private file: File = null;

    @ViewChild('file')
    fileInput: ElementRef;

    fileChanged(event: any): void {
        let file = this.fileInput.nativeElement.files[0];
        this.onChangeCallback(file);
    }

    writeValue(file: File): void {
        this.fileInput.nativeElement.files[0] = file;
    }

    registerOnChange(fn: any): void {
        this.onChangeCallback = fn;
    }

    registerOnTouched(fn: any): void {
        
    }

    setDisabledState(isDisabled: boolean): void {
        
    }
}