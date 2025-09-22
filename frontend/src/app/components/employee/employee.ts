import { Component, Input } from '@angular/core';
import { AuthService } from '../../core/services/auth/auth';
import { CommonModule } from '@angular/common';
import { ZardCardComponent } from '@shared/components/card/card.component';

@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [CommonModule, ZardCardComponent],
  templateUrl: './employee.html',
  styleUrl: './employee.css',
})
export class Employee {
  constructor(public auth: AuthService) {}
}
