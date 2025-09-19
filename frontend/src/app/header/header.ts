import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { AuthService } from '../core/services/auth/auth';
import { Observable } from 'rxjs';
import { Employee } from '../shared/models/Employee';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-header',
  imports: [AsyncPipe],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header implements OnInit {
  constructor(private readonly auth: AuthService, private readonly router: Router) {}
  user!: Observable<Employee | null>;
  isLoggedin = false;

  ngOnInit(): void {
    this.user = this.auth.user$;
    this.isLoggedin = this.verifyAuth();
  }

  handleLoginButtonClick() {
    this.router.navigate(['/login']);
  }

  handleLogoutButtonClick() {
    sessionStorage.setItem('access_token', '');
    this.auth.clearUser();
    this.isLoggedin = false;
    this.router.navigate(['/', 'login']);
  }

  verifyAuth() {
    const token = sessionStorage.getItem('access_token') as string;
    const decoded = jwtDecode(token);
    const now = Math.floor(Date.now() / 1000);
    return (decoded.exp as number) > now;
  }
}
