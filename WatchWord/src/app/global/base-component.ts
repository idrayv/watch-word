import { ToastService } from './toast/toast.service';
import { ServiceLocator } from './service-locator';

export class BaseComponent {
    private toastService: ToastService = ServiceLocator.Injector.get(ToastService);

    protected displayError(message: string, title: string = 'Error'): void {
        this.toastService.displayError(message, title);
    }

    protected displayErrors(errors: string[], title: string = 'Error'): void {
        errors.forEach((error) => this.displayError(error, title));
    }

    protected displayCustomError(html: string): void {
        this.toastService.displayCustom(html);
    }
}