import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { EmployeeApiService } from '../../core/services/api/employee-api.service';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../core/services/auth/auth';
import { Employee } from '../../components/employee/employee';
import { FormsModule } from '@angular/forms';
import { ApplyLeaveComponent } from '../../components/apply-leave/apply-leave';
import { LeavesSummary } from '../../components/leaves-summary/leaves-summary';
import { LeaveStatusNotificationComponent } from '../../components/leave-status-notification/leave-status-notification'; // New import
import { toast } from 'ngx-sonner';
import { ZardButtonComponent } from '@shared/components/button/button.component';
import { LucideAngularModule } from 'lucide-angular';
import { PlusIcon } from 'lucide-angular';

@Component({
  selector: 'app-employee-dashboard',
  standalone: true,
  imports: [
    Employee,
    FormsModule,
    CommonModule,
    LeavesSummary,
    ApplyLeaveComponent,
    LeaveStatusNotificationComponent,
    ZardButtonComponent,
    LucideAngularModule,
  ],
  templateUrl: './employee-dashboard.html',
  styleUrl: './employee-dashboard.css',
})
export class EmployeeDashboard implements OnInit {
  constructor(
    private readonly api: EmployeeApiService,
    private readonly router: ActivatedRoute,
    private auth: AuthService
  ) {}

  PlusIcon = PlusIcon;
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

  triggerToast() {
    console.log('toasting');
    toast('fdgfdhhfhhdhdh');
    console.log('did it work?');
  }

  onLeaveSubmit() {
    // Handle leave submission logic here
    console.log('Leave application submitted');
  }
}
