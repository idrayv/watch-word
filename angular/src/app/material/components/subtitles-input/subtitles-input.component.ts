import {Component, ElementRef, forwardRef, ViewChild, Output, Input, EventEmitter} from '@angular/core';
import {NG_VALUE_ACCESSOR, ControlValueAccessor, Validator, NG_VALIDATORS, AbstractControl} from '@angular/forms';
import {MaterialService} from '../../material.service';
import {VocabWord, Word} from '@shared/service-proxies/service-proxies';

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
    private serverError: string[] = [];

    @ViewChild('file')
    fileInput: ElementRef;

    @Input()
    vocabWords: VocabWord[];
    @Output()
    vocabWordsChange: EventEmitter<VocabWord[]> = new EventEmitter<VocabWord[]>();

    @Input()
    words: Word[];
    @Output()
    wordsChange: EventEmitter<Word[]> = new EventEmitter<Word[]>();

    constructor(private materialService: MaterialService) {
    }

    fileChanged() {
        abp.ui.setBusy();
        const file: File = this.fileInput.nativeElement.files[0];

        this.materialService.parseSubtitles(file).then(response => {
            abp.ui.clearBusy();
            if (response.success) {
                const responseObject = JSON.parse(response.result);
                this.vocabWordsChange.emit(responseObject.vocabWords);
                this.wordsChange.emit(responseObject.words);
                this.serverError = [];
            } else {
                this.serverError = response.error;
            }
        });
    }

    registerOnChange(fn: any): void {
        this.onChangeCallback = fn;
    }

    validate(c: AbstractControl): { [key: string]: any; } {
        if (c.value && this.serverError.length === 0) {
            return null;
        }
        return {'subtitlesInput': this.serverError};
    }

    writeValue(vocabWord: VocabWord[]): void {
    }

    registerOnValidatorChange(fn: () => void): void {
    }

    registerOnTouched(fn: any): void {
    }

    setDisabledState(isDisabled: boolean): void {
    }
}
