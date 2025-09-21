import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeamApiService } from '../../core/services/api/team-api.service';
import { AuthService } from '../../core/services/auth/auth';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-team-leave-requests',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './team-leave-requests.html',
  styleUrl: './team-leave-requests.css',
})
export class TeamLeaveRequests implements OnInit {
  teamLeaveRequests: any[] = [];
  managerId: string | null = null;

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
        this.toastr.error('You are not authorized to view this page.');
      }
    });
  }

  loadTeamLeaveRequests(): void {
    if (this.managerId) {
      this.teamApi.getManagerLeaveRequests(this.managerId).subscribe({
        next: (requests: any) => {
          console.log(requests);
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
    const comment = status === 'Approved' ? 'Approved by manager.' : 'Rejected by manager.';
    this.teamApi.updateLeaveStatus(leaveId, status, comment).subscribe({
      next: () => {
        this.toastr.success(`Leave request ${status.toLowerCase()} successfully.`);
        this.loadTeamLeaveRequests(); // Refresh the list
      },
      error: (err) => {
        this.toastr.error(`Failed to ${status.toLowerCase()} leave request.`);
        console.error(`Error updating leave status to ${status}:`, err);
      },
    });
  }
}
