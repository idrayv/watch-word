import { NgModel } from '@angular/forms';

export class ComponentValidation {
    private static capitalizeFirstLetter(word: string): string {
        return word.charAt(0).toUpperCase() + word.slice(1);
    }

    public static validationErrors(state: NgModel): string[] {
        const errors: string[] = [];
        const name = this.capitalizeFirstLetter(state.name);
        if (state.invalid) {
            for (const error in state.errors) {
                if (error) {
                    switch (error) {
                        case 'minlength':
                            errors.push(`${name} must be at least ${state.errors[error].requiredLength} characters!`);
                            break;
                        case 'required':
                            errors.push(`${name} must be filled in!`);
                            break;
                        case 'pattern':
                            if (state.name === 'email') {
                                errors.push(`${name} must be a valid email!`);
                            }
                            break;
                        case 'subtitlesInput':
                            (<string[]>state.errors[error]).forEach((er) => errors.push(er));
                            break;
                        case 'imageInput':
                            (<string[]>state.errors[error]).forEach((er) => errors.push(er));
                            break;
                    }
                }
            }
        }
        return errors;
    }
}
