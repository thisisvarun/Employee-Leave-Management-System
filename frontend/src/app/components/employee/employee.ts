import { Component, Input } from '@angular/core';
import { AuthService } from '../../core/services/auth/auth';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './employee.html',
  styleUrl: './employee.css',
})
export class Employee {
  constructor(public auth: AuthService) {}
}
