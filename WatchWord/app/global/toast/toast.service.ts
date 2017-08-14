import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';
import { ToastModel, ToastType } from './toast.models';

@Injectable()
export class ToastService {
    private toastSubject: Subject<ToastModel> = new Subject<ToastModel>();
    public toastLife = 5000;

    getObservable(): Observable<ToastModel> {
        return this.toastSubject.asObservable();
    }

    displayError(message: string, title: string = '') {
        this.toastSubject.next({
            type: ToastType.Error,
            html: '',
            message: message,
            title: title,
            toastLife: this.toastLife
        });
    }

    displayCustom(html: string) {
        this.toastSubject.next({
            type: ToastType.Custom,
            html: html,
            message: '',
            title: '',
            toastLife: this.toastLife
        });
    }
}

