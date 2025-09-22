import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth/auth';

@Component({
  selector: 'app-hr-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './hr-dashboard.html',
  styleUrl: './hr-dashboard.css'
})
export class HrDashboard {
  constructor(private readonly router: Router, private readonly auth: AuthService) {}

  navigateToAddEmployee() {
    this.auth.user$.subscribe(user => {
      if (user) {
        this.router.navigate(['/hr', user.id, 'add-employee']);
      }
    });
  }

  navigateToManageEmployees() {
    this.auth.user$.subscribe(user => {
      if (user) {
        this.router.navigate(['/hr', user.id, 'manage-employees']);
      }
    });
  }

   navigateToManageTeams() {
    this.auth.user$.subscribe(user => {
      if (user) {
        this.router.navigate(['/hr', user.id, 'manage-teams']);
      }
    });
  }
}