import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { Employee } from '../../components/employee/employee';

@Component({
  selector: 'app-employee-dashboard',
  imports: [DatePipe, Employee],
  templateUrl: './employee-dashboard.html',
  styleUrl: './employee-dashboard.css',
})
export class EmployeeDashboard {
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
}
