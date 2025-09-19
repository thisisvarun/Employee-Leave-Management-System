import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-leave-request-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './leave-request-form.html', // Corrected path
  styleUrl: './leave-request-form.css'    // Corrected path
})
export class LeaveRequestFormComponent {
  leaveForm: FormGroup;
  leaveTypes = ['Casual', 'Sick', 'Annual', 'LIEU'];

  constructor(private fb: FormBuilder) {
    this.leaveForm = this.fb.group({
      leaveType: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      description: ['', Validators.maxLength(255)]
    });
  }

  onSubmit() {
    if (this.leaveForm.valid) {
      console.log('Leave Request Submitted:', this.leaveForm.value);
      // Here, we will later call the ApiService to send data to the backend
    }
  }
}

