import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeamApiService } from '../../core/services/api/team-api.service';
import { AuthService } from '../../core/services/auth/auth';
import { ToastrService } from 'ngx-toastr';
import { TeamLeaveApplyItem } from '../team-leave-apply-item/team-leave-apply-item';

@Component({
  selector: 'app-team-leave-requests',
  standalone: true,
  imports: [CommonModule, TeamLeaveApplyItem],
  templateUrl: './team-leave-requests.html',
  styleUrl: './team-leave-requests.css',
})
export class TeamLeaveRequests implements OnInit {
  teamLeaveRequests: any[] = [];
  managerId: string | null = null;
  processingStatus: { [leaveRequestId: number]: boolean } = {};

  constructor(
    private readonly teamApi: TeamApiService,
    private readonly auth: AuthService,
    private readonly toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.auth.user$.subscribe((user) => {
      // console.log(user);
      if (user && user.role === 'Manager') {
        this.managerId = user.id.toString();
        this.loadTeamLeaveRequests();
      } else {
      }
    });
  }

  loadTeamLeaveRequests(): void {
    if (this.managerId) {
      this.teamApi.getManagerLeaveRequests(this.managerId).subscribe({
        next: (requests: any) => {
          this.teamLeaveRequests = requests;
        },
        error: (err) => {
          this.toastr.error('Failed to load team leave requests.');
          console.error('Error loading team leave requests:', err);
        },
      });
    }
  }

  updateLeaveRequestsList(event: any) {
    this.teamLeaveRequests = this.teamLeaveRequests.filter((req) => req.leaveRequestId !== event);
  }
}
