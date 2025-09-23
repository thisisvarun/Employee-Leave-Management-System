import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-leave-history',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './leave-history.html',
  styleUrls: ['./leave-history.css']
})
export class LeaveHistory implements OnInit {
  leaveHistory: any[] = [];

  constructor() { }

  ngOnInit(): void {
    // Mock data for leave history
    this.leaveHistory = [
      {
        leaveType: 'Annual',
        dates: '2024-12-20 to 2024-12-22',
        reason: 'Vacation',
        status: 'Approved'
      },
      {
        leaveType: 'Sick',
        dates: '2024-11-15',
        reason: 'Flu',
        status: 'Approved'
      },
      {
        leaveType: 'Casual',
        dates: '2024-10-05',
        reason: 'Personal',
        status: 'Rejected'
      }
    ];
  }
}