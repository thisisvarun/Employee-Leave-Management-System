import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-employee',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-employee.html',
  styleUrls: ['./add-employee.css']
})
export class AddEmployee implements OnInit {
  addEmployeeForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router
  ) {
    this.addEmployeeForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      dateOfJoining: ['', Validators.required],
      designationId: ['', Validators.required],
      teamId: [''],
      salary: ['', [Validators.required, Validators.min(0)]],
      role: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    // Here you would fetch designations and teams from your API
  }

  onSubmit(): void {
    if (this.addEmployeeForm.valid) {
      console.log('Form Submitted!', this.addEmployeeForm.value);
      // When backend is ready, you would call your API service here
      // For now, we just log to the console.
    }
  }

  onCancel(): void {
    this.router.navigate(['/hr', '1']); // Navigate back to HR dashboard
  }
}