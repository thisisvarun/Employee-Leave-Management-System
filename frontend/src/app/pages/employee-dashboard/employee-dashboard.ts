import { DatePipe, CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { Employee } from '../../components/employee/employee';
import { LeaveRequestFormComponent } from '../../components/leave-request-form/leave-request-form';
import { EmployeeDataService } from '../../core/services/employee-data';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-employee-dashboard',
  standalone: true,
  imports: [CommonModule, DatePipe, Employee, LeaveRequestFormComponent],
  templateUrl: './employee-dashboard.html',
  styleUrl: './employee-dashboard.css',
})
export class EmployeeDashboard implements OnInit {
  // Use the service to get data as Observables
  private employeeDataService = inject(EmployeeDataService);
  employee$!: Observable<any>;
  leaveStats$!: Observable<any>;
  leaveSummary$!: Observable<any>;

  today = new Date();

  ngOnInit(): void {
    // Assign the observables when the component initializes
    this.employee$ = this.employeeDataService.getEmployeeDetails();
    this.leaveStats$ = this.employeeDataService.getLeaveStats();
    this.leaveSummary$ = this.employeeDataService.getLeaveSummary();
  }
}

