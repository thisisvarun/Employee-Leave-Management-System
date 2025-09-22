import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TeamApiService } from '../../core/services/api/team-api.service';
import { finalize } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-team-leave-apply-item',
  imports: [DatePipe],
  templateUrl: './team-leave-apply-item.html',
  styleUrl: './team-leave-apply-item.css',
})
export class TeamLeaveApplyItem {
  constructor(private readonly teamApi: TeamApiService, private toastr: ToastrService) {}

  @Input() leaveItem: any;
  acceptButtonClicked: boolean = false;
  rejectButtonClicked: boolean = false;
  @Output() updateLeaveRequests = new EventEmitter<number>();

  updateLeaveStatus(leaveId: number, status: string): void {
    if (status === 'Approved') {
      this.acceptButtonClicked = true;
    } else {
      this.rejectButtonClicked = true;
    }
    const comment = status === 'Approved' ? 'Approved by manager.' : 'Rejected by manager.';
    this.teamApi
      .updateLeaveStatus(leaveId, status, comment)
      .pipe(
        finalize(() => {
          this.acceptButtonClicked = false;
          this.rejectButtonClicked = false;
        })
      )
      .subscribe({
        next: () => {
          this.toastr.success(`Leave request ${status.toLowerCase()} successfully.`);
          this.updateLeaveRequests.emit(leaveId);
        },
        error: (err) => {
          this.toastr.error(`Failed to ${status.toLowerCase()} leave request.`);
          console.error(`Error updating leave status to ${status}:`, err);
        },
      });
  }
}
