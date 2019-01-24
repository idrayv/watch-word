import {Component, ElementRef, ViewChild, Output, Input, EventEmitter, forwardRef} from '@angular/core';
import {Validator, AbstractControl, NG_VALUE_ACCESSOR, NG_VALIDATORS, ControlValueAccessor} from '@angular/forms';
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

    validate(c: AbstractControl): { [key: string]: any; } {
        if (c.value && this.serverError.length === 0) {
            return null;
        }
        return {'subtitlesInput': this.serverError};
    }

    writeValue(obj: any): void {
    }
    registerOnChange(fn: any): void {
    }
    registerOnTouched(fn: any): void {
    }
    setDisabledState?(isDisabled: boolean): void {
    }
}
