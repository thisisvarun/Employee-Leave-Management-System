import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeamApiService } from '../../core/services/api/team-api.service';
import { AuthService } from '../../core/services/auth/auth';
import { ToastrService } from 'ngx-toastr';
import { finalize, take } from 'rxjs/operators';
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
      console.log(user);
      if (user && user.role === 'Manager') {
        this.managerId = user.id.toString();
        this.loadTeamLeaveRequests();
      } else {
        // this.toastr.error('You are not authorized to view this page.');
      }
    });
  }

  loadTeamLeaveRequests(): void {
    if (this.managerId) {
      this.teamApi.getManagerLeaveRequests(this.managerId).subscribe({
        next: (requests: any) => {
          console.log('requests', requests);
          this.teamLeaveRequests = requests;
        },
        error: (err) => {
          this.toastr.error('Failed to load team leave requests.');
          console.error('Error loading team leave requests:', err);
        },
      });
    }
  }

  updateLeaveStatus(leaveId: number, status: string): void {
    this.processingStatus[leaveId] = true;
    const comment = status === 'Approved' ? 'Approved by manager.' : 'Rejected by manager.';
    this.teamApi
      .updateLeaveStatus(leaveId, status, comment)
      .pipe(finalize(() => (this.processingStatus[leaveId] = false)))
      .subscribe({
        next: () => {
          this.toastr.success(`Leave request ${status.toLowerCase()} successfully.`);
          this.teamLeaveRequests = this.teamLeaveRequests.filter(
            (req) => req.leaveRequestId !== leaveId
          );
        },
        error: (err) => {
          this.toastr.error(`Failed to ${status.toLowerCase()} leave request.`);
          console.error(`Error updating leave status to ${status}:`, err);
        },
      });
  }
}
