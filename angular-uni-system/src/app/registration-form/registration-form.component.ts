import { Component } from '@angular/core';

@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.css']
})

export class RegistrationFormComponent {
  name: string;
  email: string;
  password: string;

  onSubmit() {
    console.log('Registration form submitted!');
    console.log(`Name: ${this.name}`);
    console.log(`Email: ${this.email}`);
    console.log(`Password: ${this.password}`);
  }
}
