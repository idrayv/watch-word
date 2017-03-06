import { Component } from '@angular/core';

@Component({
  selector: 'watch-word',
  template: `<h1>Hello {{name}}!</h1>`,
})

export class AppComponent { name = 'WatchWord'; }