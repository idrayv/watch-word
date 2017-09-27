import { ToastOptions } from 'ng2-toastr/ng2-toastr';

export class ToastModel {
    type: ToastType;
    title: string;
    message: string;
    html: string;
    toastLife: number;
}

export enum ToastType {
    Error, Custom
}

export class CustomOption extends ToastOptions {
    positionClass = 'toast-bottom-center';
    dismiss = 'controlled';
}
