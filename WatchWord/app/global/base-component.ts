import { ToastService } from './toast/toast.service';
import { ServiceLocator } from './service-locator';

export class BaseComponent {
    private toastService: ToastService = ServiceLocator.Injector.get(ToastService);

    protected displayConnectionError(): void {
        this.toastService.displayError('Please try again later.', 'Server unavailable');
    }

    protected displayError(message: string, title: string = ''): void {
        this.toastService.displayError(message, title);
    }

    protected displayCustomError(html: string): void {
        this.toastService.displayCustom(html);
    }
}