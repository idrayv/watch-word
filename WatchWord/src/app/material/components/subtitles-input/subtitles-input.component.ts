import { Component, ElementRef, forwardRef, ViewChild, Output, Input, EventEmitter } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor, Validator, NG_VALIDATORS, AbstractControl } from '@angular/forms';
import { MaterialService } from '../../material.service';
import { VocabWord, Word } from '../../material.models';
import { SpinnerService } from '../../../global/spinner/spinner.service';

@Component({
    selector: 'ww-subtitles-input',
    templateUrl: 'subtitles-input.template.html',
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

    constructor(private materialService: MaterialService, private spinner: SpinnerService) {
    }

    @ViewChild('file') fileInput: ElementRef;

    @Input() vocabWords: VocabWord[];
    @Output() vocabWordsChange: EventEmitter<VocabWord[]> = new EventEmitter<VocabWord[]>();

    @Input() words: Word[];
    @Output() wordsChange: EventEmitter<Word[]> = new EventEmitter<Word[]>();

    fileChanged() {
        this.spinner.displaySpinner(true);
        const file: File = this.fileInput.nativeElement.files[0];

        this.materialService.parseSubtitles(file).then(response => {
            this.spinner.displaySpinner(false);
            if (response.success) {
                this.vocabWordsChange.emit(response.vocabWords);
                this.wordsChange.emit(response.words);
                this.serverErrors = [];
            } else {
                this.serverErrors = response.errors;
            }
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

    writeValue(VocabWord: VocabWord[]): void {}

    registerOnValidatorChange(fn: () => void): void {}

    registerOnTouched(fn: any): void {}

    setDisabledState(isDisabled: boolean): void {}
}
