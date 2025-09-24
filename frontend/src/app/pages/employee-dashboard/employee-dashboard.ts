import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { EmployeeApiService } from '../../core/services/api/employee-api.service';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../core/services/auth/auth';
import { Employee } from '../../components/employee/employee';
import { FormsModule } from '@angular/forms';
import { ApplyLeaveComponent } from '../../components/apply-leave/apply-leave';
import { LeavesSummary } from '../../components/leaves-summary/leaves-summary';
import { LeaveStatusNotificationComponent } from '../../components/leave-status-notification/leave-status-notification';
import { toast } from 'ngx-sonner';
import { ZardButtonComponent } from '@shared/components/button/button.component';
import { LucideAngularModule } from 'lucide-angular';
import { PlusIcon } from 'lucide-angular';
import { AgPolarChartOptions } from 'ag-charts-community';
import { LeaveSummaryChart } from 'src/app/components/leave-summary-chart/leave-summary-chart';
import { LeaveApiService } from 'src/app/core/services/api/leave-api.service';
import { ZardDialogService } from '@shared/components/dialog/dialog.service';
import { ApplyLeaveDialog } from 'src/app/components/apply-leave-dialog/apply-leave-dialog';
import { DialogData } from '@shared/models/DialogData';

@Component({
  selector: 'app-employee-dashboard',
  standalone: true,
  imports: [
    Employee,
    FormsModule,
    CommonModule,
    ApplyLeaveComponent,
    LeaveStatusNotificationComponent,
    ZardButtonComponent,
    LucideAngularModule,
    LeaveSummaryChart,
  ],
  templateUrl: './employee-dashboard.html',
  styleUrl: './employee-dashboard.css',
})
export class EmployeeDashboard implements OnInit {
  public options;
  leaveSummary: any = {};

  constructor(
    private readonly api: EmployeeApiService,
    private readonly router: ActivatedRoute,
    private auth: AuthService,
    private leaveApi: LeaveApiService
  ) {
    this.options = {
      data: this.data,
      title: {
        text: 'Leaves Summary',
        color: '#fff',
        fontSize: 24,
        fontWeight: 'semibold',
      },
      series: [
        {
          type: 'donut',
          calloutLabelKey: 'asset',
          angleKey: 'amount',
          calloutLabel: {
            color: 'white',
            fontSize: 12,
          },
        },
      ],
      padding: { top: 10, right: 10, bottom: 10, left: 10 },
      legend: {
        enabled: false,
      },
      background: {
        fill: 'oklch(0.21 0.006 285.885)',
      },
    } as unknown as AgPolarChartOptions;
  }

  data = [
    { asset: 'Remaining', amount: 30 },
    { asset: 'Pending', amount: 55 },
    { asset: 'Used', amount: 90 },
  ];

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

    this.auth.user$.subscribe((user) => {
      if (user) {
        this.leaveApi.getLeaveSummary(user.id.toString()).subscribe((summary: any) => {
          this.leaveSummary = summary;
          // console.log('STRUCTURE', summary);
        });
      }
    });
  }

  private dialogService = inject(ZardDialogService);

  openDialog() {
    this.dialogService.create({
      zTitle: 'Apply Leave',
      zDescription: ``,
      zContent: ApplyLeaveDialog,
      zData: {
        name: 'Samuel Rizzon',
        username: '@samuelrizzondev',
      } as DialogData,
      zOkText: 'Save changes',
      zOnOk: (instance) => {
        // console.log('Form submitted:', instance.form.value);
      },
    });
  }

  triggerToast() {}

  onLeaveSubmit() {
    // console.log('Leave application submitted');
  }
}
