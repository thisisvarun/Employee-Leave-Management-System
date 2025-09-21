import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { finalize, take } from 'rxjs/operators';
import { LeaveApiService } from '../../core/services/api/leave-api.service';
import { AuthService } from '../../core/services/auth/auth';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-apply-leave',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './apply-leave.html',
  styleUrls: ['./apply-leave.css'],
})
export class ApplyLeaveComponent implements OnInit {
  leaveForm: FormGroup;
  errorMessage: string | null = null;
  isSubmitting = false;

  constructor(
    private fb: FormBuilder,
    private leaveApi: LeaveApiService,
    private auth: AuthService,
    private toastr: ToastrService
  ) {
    this.leaveForm = this.fb.group({
      leaveType: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      reason: ['', Validators.required],
    });
  }

  ngOnInit(): void {}

  onLeaveSubmit() {
    this.errorMessage = null;
    if (this.leaveForm.valid && !this.isSubmitting) {
      this.isSubmitting = true;
      this.auth.user$.pipe(take(1)).subscribe(user => {
        if (user) {
          const formValue = this.leaveForm.value;
          const leaveData = {
            employeeId: user.id,
            leaveType: formValue.leaveType,
            description: formValue.reason,
            dates: this.getDatesArray(formValue.startDate, formValue.endDate)
          };

          this.leaveApi.applyLeave(leaveData).pipe(
            finalize(() => this.isSubmitting = false)
          ).subscribe({
            next: (response) => {
              this.toastr.success('Leave applied successfully!');
              this.leaveForm.reset();
            },
            error: (error) => {
              this.toastr.error(error.error.message || 'An unexpected error occurred.');
            }
          });
        }
      });
    }
  }

  private getDatesArray(startDate: string, endDate: string): { date: string; hours: number }[] {
    const dates = [];
    let currentDate = new Date(startDate);
    const end = new Date(endDate);

    while (currentDate <= end) {
      dates.push({ date: currentDate.toISOString().split('T')[0], hours: 8 });
      currentDate.setDate(currentDate.getDate() + 1);
    }
    return dates;
  }
}