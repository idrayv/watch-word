import { Injectable } from '@angular/core';
import { ModalComponent } from './modal.component';

@Injectable()
export class ModalService {
    private modals: ModalComponent[] = [];

    public add(modal: ModalComponent): void {
        this.modals.push(modal);
    }

    public remove(id: string): void {
        const index = this.modals.findIndex(elem => elem.id === id);
        if (index !== -1) {
            this.modals.splice(index, 1);
        }
    }

    public showModal(id: string): void {
        const modal = this.modals.find(elem => elem.id === id);
        if (modal) {
            modal.show();
        }
    }

    public hideModal(id: string): void {
        const modal = this.modals.find(elem => elem.id === id);
        if (modal) {
            modal.hide();
        }
    }
}
