import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-header',
  imports: [],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {
  constructor(private readonly router: Router) {}

  handleLoginButtonClick() {
    this.router.navigate(['/login']);
  }

  handleLogoutButtonClick() {
    sessionStorage.setItem('access_token', '');
    this.router.navigate(['/', 'login']);
  }

  verifyAuth() {
    const token = sessionStorage.getItem('access_token') as string;
    const decoded = jwtDecode(token);
    const now = Math.floor(Date.now() / 1000);
    return (decoded.exp as number) > now;
  }
}
