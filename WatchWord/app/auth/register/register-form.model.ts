import { FormControl, FormGroup, Validators } from '@angular/forms';

export class RegisterFormControl extends FormControl {
    public label: string;
    public modelProperty: string;

    constructor(label: string, property: string, value: any, validator: any) {
        super(value, validator);
        this.label = label;
        this.modelProperty = property;
    }

    getValidationMessages() {
        let messages: string[] = [];

        if (this.errors) {
            for (let errorName in this.errors) {
                switch (errorName) {
                    case 'required':
                        messages.push(`${this.label} must be filled in!`);
                        break;
                    case 'minlength':
                        messages.push(`${this.label} must be at least ${this.errors['minlength'].requiredLength} characters!`);
                        break;
                    case 'maxlength':
                        messages.push(`${this.label} must be less than ${this.errors['maxlength'].requiredLength} characters!`);
                        break;
                    case 'pattern':
                        messages.push(`${this.label} is invalid!`);
                        break;
                }
            }
        }

        return messages;
    }
}

export class RegisterFormGroup extends FormGroup {
    constructor() {
        super({
            login: new RegisterFormControl('Login', 'login', '', Validators.compose([
                Validators.required,
                Validators.maxLength(20)
            ])),
            password: new RegisterFormControl('Password', 'password', '', Validators.compose([
                Validators.required,
                Validators.minLength(4)
            ])),
            email: new RegisterFormControl('Email', 'email', '', Validators.compose([
                Validators.required,
                Validators.pattern(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)
            ]))
        })
    }

    get productControls(): RegisterFormControl[] {
        return Object.keys(this.controls)
            .map(k => this.controls[k] as RegisterFormControl);
    }
}