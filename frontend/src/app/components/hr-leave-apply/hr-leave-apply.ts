import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { HrApiService } from '../../core/services/api/hr-api.service';
import { finalize } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-hr-leave-apply',
  imports: [DatePipe],
  templateUrl: './hr-leave-apply.html',
  styleUrl: './hr-leave-apply.css'
})
export class HrLeaveApply {
  constructor(private readonly hrApi: HrApiService, private toastr: ToastrService) {}

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
    const comment = status === 'Approved' ? 'Approved by Hr.' : 'Rejected by Hr.';
    this.hrApi
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
