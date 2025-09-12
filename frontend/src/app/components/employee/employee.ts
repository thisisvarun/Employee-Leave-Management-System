import { Component } from '@angular/core';

@Component({
  selector: 'app-employee',
  imports: [],
  templateUrl: './employee.html',
  styleUrl: './employee.css',
})
export class Employee {
  employee = {
    name: 'Stephan Peralt',
    role: 'Senior Product Designer',
    team: 'UI/UX Design',
    phone: '+1 324 3453 545',
    email: 'Steperde124@example.com',
    office: 'Douglas Martini',
    joined: '15 Jan 2024',
    avatar: '/assets/image.jpg',
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
