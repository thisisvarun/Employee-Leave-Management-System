import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../core/services/auth/auth';
import { ToastrService } from 'ngx-toastr';
import { HrApiService } from 'src/app/core/services/api/hr-api.service';
import { HrLeaveApply } from '../hr-leave-apply/hr-leave-apply';

@Component({
  selector: 'app-all-leave-requests',
  standalone: true,
  imports: [
    CommonModule,
    HrLeaveApply
  ],
  templateUrl: './all-leave-requests.html',
  styleUrl: './all-leave-requests.css'
})
export class AllLeaveRequests implements OnInit {
  allLeaveRequests: any[] = [];
  hrId: string | null = null;
  processingStatus: { [leaveRequestId: number]: boolean } = {};

  constructor(
    private readonly hrApi: HrApiService,
    private readonly auth: AuthService,
    private readonly toastr: ToastrService
  ) {}

  // ngOnInit(): void {
  //   throw new Error('Method not implemented.');
  // }

  ngOnInit(): void {
    this.auth.user$.subscribe((user) => {
      console.log(user);
      if (user && user.role === 'Hr') {
        this.hrId = user.id.toString();
        this.loadAllLeaveRequests();
      } else {
        // this.toastr.error('You are not authorized to view this page.');
      }
    });
  }

  loadAllLeaveRequests(): void {
    if (this.hrId) {
      this.hrApi.getHrLeaveRequests(this.hrId).subscribe({
        next: (requests: any) => {
          console.log('XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX\n requests', requests);
          this.allLeaveRequests = requests;
        },
        error: (err) => {
          this.toastr.error('Failed to load all leave requests.');
          console.error('Error loading all leave requests:', err);
        },
      });
    }
  }

  updateLeaveRequestsList(event: any) {
    this.allLeaveRequests = this.allLeaveRequests.filter((req) => req.leaveRequestId !== event);
  }
}
