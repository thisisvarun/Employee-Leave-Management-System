import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { finalize, take } from 'rxjs/operators';
import { AuthService } from '../../core/services/auth/auth';
import { ToastrService } from 'ngx-toastr';
import { LucideAngularModule, PlusIcon } from 'lucide-angular';
import { EmployeeApiService } from '../../core/services/api/employee-api.service';

@Component({
  selector: 'app-create-employee',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, LucideAngularModule],
  templateUrl: './create-employee.html',
  styleUrls: ['./create-employee.css']
})
export class CreateEmployee implements OnInit {
  employeeForm: FormGroup;
  errorMessage: string | null = null;
  isSubmitting = false;
  PlusIcon = PlusIcon;

  constructor(
    private fb: FormBuilder,
    private employeeApi: EmployeeApiService,
    private auth: AuthService,
    private toastr: ToastrService
  ) {
    this.employeeForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.maxLength(100)]],
      lastName: ['', [Validators.required, Validators.maxLength(100)]],
      email: ['', [Validators.required, Validators.email, Validators.maxLength(150)]],
      phone: ['', [Validators.maxLength(20)]],
      teamId: [null],
      designationId: [null, Validators.required],
      salary: [0, [Validators.required, Validators.min(0)]],
      dateOfJoining: ['', Validators.required],
      active: [true, Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      role: ['', Validators.required]
    });
  }

  ngOnInit(): void { }

  onEmployeeSubmit() {
    this.errorMessage = null;
    if (this.employeeForm.valid && !this.isSubmitting) {
      this.isSubmitting = true;

      const formValue = this.employeeForm.value;
      const employeeData = {
        firstName: formValue.firstName,
        lastName: formValue.lastName,
        email: formValue.email,
        phone: formValue.phone,
        teamId: formValue.teamId,
        designationId: formValue.designationId,
        salary: formValue.salary,
        dateOfJoining: formValue.dateOfJoining,
        active: formValue.active,
        password: formValue.password,
        role: formValue.role
      };

      this.employeeApi.createEmployee(employeeData).pipe(
        finalize(() => this.isSubmitting = false),
        take(1)
      ).subscribe({
        next: (response) => {
          this.toastr.success('Employee created successfully!');
          this.employeeForm.reset({
            active: true,
            salary: 0
          });
        },
        error: (error) => {
          this.toastr.error(error.error?.message || 'An unexpected error occurred.');
        }
      });
    } else {
      this.errorMessage = 'Please fill all required fields correctly.';
    }
  }
}
