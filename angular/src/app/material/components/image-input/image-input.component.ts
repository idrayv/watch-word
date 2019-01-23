import {Component, ElementRef, ViewChild, OnInit, Input, Output, EventEmitter} from '@angular/core';
import {Validator, AbstractControl} from '@angular/forms';
import {MaterialService} from '../../material.service';

@Component({
    selector: 'ww-image-input[mimeTypes]',
    templateUrl: 'image-input.template.html'
})
export class ImageInputComponent implements Validator, OnInit {
    private error: string[] = [];
    private types: string[] = [];

    @ViewChild('file') fileInput: ElementRef;

    @Input()
    image: string;
    @Output()
    imageChange: EventEmitter<string> = new EventEmitter<string>();

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

            this.imageChange.emit('');
        }
    }

    callService(file: File): void {
        abp.ui.setBusy();
        this.materialService.parseImage(file).then(response => {
            abp.ui.clearBusy();
            if (response.success) {
                this.error = [];

            } else if (this.error) {
                this.addErrorsAndCleanInput(response.error);
            } else {
                this.addErrorsAndCleanInput(['Server unavailable.']);
            }

            this.imageChange.emit(response.result);
        });
    }

    addErrorsAndCleanInput(error: string[]): void {
        this.error = error;
        this.fileInput.nativeElement.value = null;
    }

    // TODO: Fix validation
    validate(c: AbstractControl): { [key: string]: any; } {
        if (c.value && this.error.length === 0) {
            return null;
        }
        return {'imageInput': this.error};
    }
}
