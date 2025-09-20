import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../../core/services/api/api';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-form',
  imports: [ReactiveFormsModule],
  templateUrl: './login-form.html',
  styleUrl: './login-form.css',
})
export class LoginForm {
  loginForm: FormGroup;
  errorMessage: string = '';

  constructor(
    private readonly fb: FormBuilder,
    private readonly api: ApiService,
    private readonly router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      this.api.loginUser(this.loginForm.value.email, this.loginForm.value.password).subscribe({
        next: (res: any) => {
          sessionStorage.setItem('access_token', res.token);
          console.log('Result: ', res);
          if (res.role.toLowerCase() == 'employee') {
            this.router.navigate(['/', 'employee', res.employeeId]);
          }
        },
        error: (err) => {
          console.log('[ERROR]', err);
          this.errorMessage = "The username and password doesn't match";
        },
      });
    }
  }
}
