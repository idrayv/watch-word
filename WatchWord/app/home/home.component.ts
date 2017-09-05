import { Component } from '@angular/core';
import { BaseComponent } from '../global/base-component';

@Component({
    templateUrl: 'app/home/home.template.html'
})

export class HomeComponent extends BaseComponent {
    name = 'Home';
}