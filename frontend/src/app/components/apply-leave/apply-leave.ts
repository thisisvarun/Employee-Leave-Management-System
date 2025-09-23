import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, FormArray, AbstractControl, ValidatorFn } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { finalize, take } from 'rxjs/operators';
import { LeaveApiService } from '../../core/services/api/leave-api.service';
import { AuthService } from '../../core/services/auth/auth';
import { ToastrService } from 'ngx-toastr';
import { LeavesSummary } from "../leaves-summary/leaves-summary";
import { PlusIcon, LucideAngularModule } from 'lucide-angular';

@Component({
  selector: 'app-apply-leave',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, LeavesSummary, LucideAngularModule],
  templateUrl: './apply-leave.html',
  styleUrls: ['./apply-leave.css'],
})
export class ApplyLeaveComponent implements OnInit {
  leaveForm: FormGroup;
  errorMessage: string | null = null;
  isSubmitting = false;
  PlusIcon = PlusIcon;

  constructor(
    private fb: FormBuilder,
    private leaveApi: LeaveApiService,
    private auth: AuthService,
    private toastr: ToastrService
  ) {
    this.leaveForm = this.fb.group({
      leaveType: ['', Validators.required],
      reason: ['', Validators.required],
      dates: this.fb.array([this.createDateGroup()], this.uniqueDatesValidator())
    });
  }

  ngOnInit(): void {}

  get dates(): FormArray {
    return this.leaveForm.get('dates') as FormArray;
  }

  createDateGroup(): FormGroup {
    return this.fb.group({
      date: ['', [Validators.required, this.futureDateValidator()]],
      hours: [8, [Validators.required, Validators.min(2), Validators.max(8)]]
    });
  }

  addDateGroup(): void {
    this.dates.push(this.createDateGroup());
  }

  removeDateGroup(index: number): void {
    this.dates.removeAt(index);
  }

  futureDateValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (!control.value) {
        return null;
      }
      const selectedDate = new Date(control.value);
      selectedDate.setHours(0, 0, 0, 0);
      const today = new Date();
      today.setHours(0, 0, 0, 0);
      return selectedDate >= today ? null : { futureDate: { value: control.value } };
    };
  }

  uniqueDatesValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const datesArray = control as FormArray;
      const dates = datesArray.controls.map(group => group.get('date')?.value);
      const uniqueDates = new Set(dates.filter(d => d));
      return dates.length === uniqueDates.size ? null : { uniqueDates: true };
    };
  }

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
            dates: formValue.dates
          };

          this.leaveApi.applyLeave(leaveData).pipe(
            finalize(() => this.isSubmitting = false)
          ).subscribe({
            next: (response) => {
              this.toastr.success('Leave applied successfully!');
              this.leaveForm.reset();
              this.dates.clear(); // Clear existing controls
              this.addDateGroup(); // Add one initial control
            },
            error: (error) => {
              this.toastr.error(error.error.message || 'An unexpected error occurred.');
            }
          });
        }
      });
    }
  }
}