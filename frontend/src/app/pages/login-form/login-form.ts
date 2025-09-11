import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../../core/services/api/api';

@Component({
  selector: 'app-login-form',
  imports: [ReactiveFormsModule],
  templateUrl: './login-form.html',
  styleUrl: './login-form.css',
})
export class LoginForm {
  loginForm: FormGroup;
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private api: ApiService) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.pattern('^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#$%^&*()_+=]).+$'),
        ],
      ],
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      console.log(this.loginForm.value);
      this.api.loginUser(this.loginForm.value.email, this.loginForm.value.password).subscribe({
        next: (res) => {
          console.log('Result: ', res);
        },
        error: (err) => {
          console.log('[ERROR]', err);
          this.errorMessage = err.statusText;
        },
      });
    }
  }
}
