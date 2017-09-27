import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class SpinnerService {
    public spinnerStatus: Subject<boolean> = new Subject<boolean>();

    displaySpinner(value: boolean) {
        this.spinnerStatus.next(value);
    }

    public getSpinnerObservable(): Observable<boolean> {
        return this.spinnerStatus.asObservable();
    }
}
