import { CSP_NONCE, Injectable } from '@angular/core';
import { Employee } from '../../../shared/models/Employee';
import { BehaviorSubject } from 'rxjs';

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
