import { CSP_NONCE, Injectable } from '@angular/core';
import { Employee } from '../../../shared/models/Employee';
import { BehaviorSubject } from 'rxjs';
import { REMOVE_STYLES_ON_COMPONENT_DESTROY } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private user = new BehaviorSubject<Employee | null>(null);
  user$ = this.user.asObservable();

  updateUser(user: Employee) {
    this.user.next(user);
    this.user$.subscribe((d) => console.log(d));
  }

  clearUser() {
    this.user.next(null);
  }
}
