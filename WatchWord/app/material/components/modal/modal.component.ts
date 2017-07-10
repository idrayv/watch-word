import { OnInit, OnDestroy, Component, ElementRef, ViewChild, Input } from '@angular/core';
import { ModalService } from './modal.service';

@Component({
    selector: 'modal',
    templateUrl: 'app/material/components/modal/modal.template.html',
    styles: [`
        .modal {
          position: fixed;
          top: 50%;
          left: 50%;
          transform: translate(-50%, -50%);
          width: 600px;
          max-width: 100%;
          background-color: yellow;
        }
        .modalHide {
            display: none;
        }
    `]
})

export class ModalComponent implements OnInit, OnDestroy {
    private element: any;
    @Input('id')
    id: string;
    @ViewChild('modalBody')
    modalBody: ElementRef;

    constructor(private service: ModalService) { }

    public show(): void {
        this.modalBody.nativeElement.classList.remove('modalHide');
    }

    public hide(): void {
        this.modalBody.nativeElement.classList.add('modalHide');
    }

    ngOnInit(): void {
        this.service.add(this);
    }

    ngOnDestroy(): void {
        this.service.remove(this.id);
    }
}