import {Component, ElementRef, ViewChild, Output, Input, EventEmitter} from '@angular/core';
import {Validator, AbstractControl} from '@angular/forms';
import {MaterialService} from '../../material.service';
import {VocabWord, Word} from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'ww-subtitles-input',
    templateUrl: 'subtitles-input.template.html',
})
export class SubtitlesInputComponent implements Validator {
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
}
