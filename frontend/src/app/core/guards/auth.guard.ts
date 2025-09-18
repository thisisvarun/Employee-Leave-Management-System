import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { ApiService } from '../services/api/api';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private readonly apiService: ApiService, private readonly router: Router) {}

  canActivate() {
    const token = sessionStorage.getItem('access_token') as string;
    const decoded = jwtDecode(token);
    const now = Math.floor(Date.now() / 1000);
    return (decoded.exp as number) > now;
  }
}
