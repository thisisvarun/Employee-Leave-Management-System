import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Employee } from '../models/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeDataService {

  // We will replace this with a real API call later
  getEmployeeDetails(): Observable<any> {
    const mockEmployee = {
      name: 'Stephan Peralt',
      role: 'Senior Product Designer',
      team: 'UI/UX Design',
      phone: '+1 324 3453 545',
      email: 'Steperde124@example.com',
      office: 'Douglas Martini',
      joined: '15 Jan 2024',
      avatar: '/frontend/assets/image.png',
    };
    return of(mockEmployee); // 'of' creates an Observable that emits the mock data
  }

  getLeaveStats(): Observable<any> {
    const mockLeaveStats = {
      onTime: 1254,
      late: 32,
      workFromHome: 658,
      absent: 14,
      sickLeave: 68,
    };
    return of(mockLeaveStats);
  }

  getLeaveSummary(): Observable<any> {
    const mockLeaveSummary = {
      total: 16,
      taken: 10,
      absent: 2,
      request: 0,
      workedDays: 240,
      lossOfPay: 2,
    };
    return of(mockLeaveSummary);
  }

  constructor() { }
}
