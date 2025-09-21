import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthApiService } from '../../core/services/api/auth-api.service';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth/auth';

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
    private readonly api: AuthApiService,
    private readonly router: Router,
    private readonly auth: AuthService
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      this.api.loginUser(this.loginForm.value.email, this.loginForm.value.password).subscribe({
        next: (result: any) => {
          sessionStorage.setItem('access_token', result.token);
          console.log('Result: ', result);
          if (result.role.toLowerCase() == 'employee') {
            this.router.navigate(['/', 'employee', result.employeeId]);
          } else if (result.role.toLowerCase() == 'manager') {
            this.router.navigate(['/', 'manager', result.employeeId]);
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
