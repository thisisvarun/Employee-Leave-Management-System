import { DatePipe, CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../core/services/api/api';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../core/services/auth/auth';
import { Employee } from '../../components/employee/employee';
import { FormsModule } from '@angular/forms';

import { Employee as EmployeeModel } from '../../shared/models/Employee';

@Component({
  selector: 'app-employee-dashboard',
  standalone: true,
  imports: [DatePipe, Employee, FormsModule, CommonModule],
  templateUrl: './employee-dashboard.html',
  styleUrl: './employee-dashboard.css',
})
export class EmployeeDashboard implements OnInit {
  constructor(
    private readonly api: ApiService,
    private readonly router: ActivatedRoute,
    private auth: AuthService
  ) {}

  employee = {
    name: 'Stephan Peralt',
    role: 'Senior Product Designer',
    team: 'UI/UX Design',
    phone: '+1 324 3453 545',
    email: 'Steperde124@example.com',
    office: 'Douglas Martini',
    joined: '15 Jan 2024',
    avatar: '/frontend/assets/image.png',
  };

  leaveStats = {
    onTime: 1254,
    late: 32,
    workFromHome: 658,
    absent: 14,
    sickLeave: 68,
  };

  leaveSummary = {
    total: 16,
    taken: 10,
    absent: 2,
    request: 0,
    workedDays: 240,
    lossOfPay: 2,
  };

  today = new Date();

  ngOnInit(): void {
    console.log('[router value]', this.router.snapshot.paramMap.get('id'));
    this.api.getEmployeeById(this.router.snapshot.paramMap.get('id')!).subscribe({
      next: (result: any) => {
        this.auth.updateUser({
          id: result.employee_Id,
          firstName: result.first_Name,
          lastName: result.last_Name,
          email: result.email,
          phone: result.phone,
          teamdId: result.team_Id,
          salary: result.salary,
          designationId: result.designation_Id,
          dateOfJoining: result.date_Of_Joining,
          active: result.active,
          role: result.role,
        });
      },
      error: (error: any) => {
        console.log('[GET EMPLOYEE BY ID ERROR]', error);
      },
    });
  }

  onLeaveSubmit() {
    // Handle leave submission logic here
    console.log('Leave application submitted');
  }
}
