import { DatePipe, CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { EmployeeApiService } from '../../core/services/api/employee-api.service';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../core/services/auth/auth';
import { Employee } from '../../components/employee/employee';
import { FormsModule } from '@angular/forms';
import { ApplyLeaveComponent } from '../../components/apply-leave/apply-leave';
import { LeavesSummary } from '../../components/leaves-summary/leaves-summary';
import { TeamLeaveRequests } from '../../components/team-leave-requests/team-leave-requests';
import { LeaveStatusNotificationComponent } from '../../components/leave-status-notification/leave-status-notification'; // New import
import { LucideAngularModule, Plus } from 'lucide-angular';

@Component({
  selector: 'app-manager-dashboard',
  standalone: true,
  imports: [
    LucideAngularModule,
    Employee,
    FormsModule,
    CommonModule,
    LeavesSummary,
    ApplyLeaveComponent,
    TeamLeaveRequests,
    LeaveStatusNotificationComponent,
  ], // Added LeaveStatusNotificationComponent
  templateUrl: './manager-dashboard.html',
  styleUrl: './manager-dashboard.css',
})
export class ManagerDashboard implements OnInit {
  constructor(
    private readonly api: EmployeeApiService,
    private readonly router: ActivatedRoute,
    private auth: AuthService
  ) {}

  readonly PlusIcon = Plus;

  leaveStats = {
    onTime: 1254,
    late: 32,
    workFromHome: 658,
    absent: 14,
    sickLeave: 68,
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
