import { Component, ElementRef, ViewChild, OnInit, Input, Output, EventEmitter, forwardRef } from '@angular/core';
import { Validator, AbstractControl, NG_VALUE_ACCESSOR, NG_VALIDATORS, ControlValueAccessor } from '@angular/forms';
import { MaterialService } from '../../material.service';

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

  writeValue(obj: any): void {
  }

  registerOnChange(fn: any): void {
  }

  registerOnTouched(fn: any): void {
  }

  setDisabledState?(isDisabled: boolean): void {
  }
}
