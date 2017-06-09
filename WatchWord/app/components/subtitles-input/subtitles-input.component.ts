﻿import { Component, ElementRef, forwardRef, Output, ViewChild } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor, Validator, NG_VALIDATORS, AbstractControl } from '@angular/forms';
import { MaterialService } from "../../material/material.service";
import { Word } from '../../material/material.models';

@Component({
    selector: 'subtitles-input',
    templateUrl: "app/components/subtitles-input/subtitles-input.template.html",
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => SubtitlesInputComponent),
            multi: true
        },
        {
            provide: NG_VALIDATORS,
            useExisting: forwardRef(() => SubtitlesInputComponent),
            multi: true
        }
    ]
})
export class SubtitlesInputComponent implements ControlValueAccessor, Validator {
    private onChangeCallback: Function;
    private serverErrors: Array<string> = [];

    constructor(private materialService: MaterialService) { }

    @ViewChild('file')
    fileInput: ElementRef;

    writeValue(subbtitles: Array<Word>): void {

    }

    fileChanged() {
        let file: File = this.fileInput.nativeElement.files[0];
        let words: Array<Word> = [];
        this.materialService.parseSubtitles(file).subscribe(
            response => {
                if (response.success) {
                    words = response.words.map((w) => { let word = new Word(); word.theWord = w.theWord; word.count = w.count; return word; });
                    this.serverErrors = [];

                } else {
                    this.serverErrors = response.errors;
                }
                this.onChangeCallback(words);
            },
            err => {
                this.serverErrors = new Array<string>("Connection error");
                this.onChangeCallback(words);
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
        return { "subtitlesInput": this.serverErrors };
    }

    registerOnValidatorChange(fn: () => void): void {

    }
}