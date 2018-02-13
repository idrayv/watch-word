import {OnInit, OnDestroy, Component, ElementRef, ViewChild, Input} from '@angular/core';
import {ModalService} from './modal.service';

@Component({
    selector: 'ww-modal',
    templateUrl: 'modal.template.html'
})

export class ModalComponent implements OnInit, OnDestroy {
    @Input('id') id: string;
    @ViewChild('modalBody') modalBody: ElementRef;

    constructor(private service: ModalService) {
    }

    public show(): void {
        this.modalBody.nativeElement.classList.add('active');
    }

    public hide(): void {
        this.modalBody.nativeElement.classList.remove('active');
    }

    ngOnInit(): void {
        this.service.add(this);
    }

    ngOnDestroy(): void {
        this.service.remove(this.id);
    }
}
