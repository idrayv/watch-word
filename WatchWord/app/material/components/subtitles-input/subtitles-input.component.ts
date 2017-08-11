import { Component, ElementRef, forwardRef, ViewChild } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor, Validator, NG_VALIDATORS, AbstractControl } from '@angular/forms';
import { MaterialService } from '../../material.service';
import { Word, WordComposition } from '../../material.models';
import { SpinnerService } from '../../../global/spinner/spinner.service';

@Component({
    selector: 'subtitles-input',
    templateUrl: 'app/material/components/subtitles-input/subtitles-input.template.html',
    providers: [{
        provide: NG_VALUE_ACCESSOR,
        useExisting: forwardRef(() => SubtitlesInputComponent),
        multi: true
    }, {
        provide: NG_VALIDATORS,
        useExisting: forwardRef(() => SubtitlesInputComponent),
        multi: true
    }]
})

export class SubtitlesInputComponent implements ControlValueAccessor, Validator {
    private onChangeCallback: Function;
    private serverErrors: string[] = [];

    constructor(private materialService: MaterialService, private spinner: SpinnerService) { }

    @ViewChild('file') fileInput: ElementRef;

    fileChanged() {
        this.spinner.displaySpinner(true);
        let file: File = this.fileInput.nativeElement.files[0];
        let words: WordComposition[];
        this.materialService.parseSubtitles(file).then(response => {
            this.spinner.displaySpinner(false);
            if (response.success) {
                words = this.materialService.composeWordWithVocabulary(response.words, response.vocabWords);
                this.serverErrors = [];
            } else {
                this.serverErrors = response.errors;
            }
            this.onChangeCallback(words);
        });
    }

    registerOnChange(fn: any): void {
        this.onChangeCallback = fn;
    }

    validate(c: AbstractControl): { [key: string]: any; } {
        if (c.value && this.serverErrors.length === 0) {
            return null;
        }
        return { 'subtitlesInput': this.serverErrors };
    }

    writeValue(subbtitles: Word[]): void {}

    registerOnValidatorChange(fn: () => void): void {}

    registerOnTouched(fn: any): void {}

    setDisabledState(isDisabled: boolean): void {}
}