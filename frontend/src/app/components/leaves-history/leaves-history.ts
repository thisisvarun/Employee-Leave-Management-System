import { Component, OnInit } from '@angular/core';
import { LeaveApiService } from '../../core/services/api/leave-api.service';
import { AuthService } from '../../core/services/auth/auth';
import { CommonModule } from '@angular/common';
import { LeaveHistoryItem } from '../leave-history-item/leave-history-item';
import { Leave } from '@shared/models/Leave';

@Component({
  selector: 'app-leaves-history',
  standalone: true,
  imports: [CommonModule, LeaveHistoryItem],
  templateUrl: './leaves-history.html',
  styleUrls: ['./leaves-history.css'],
})
export class LeavesHistory implements OnInit {
  leaveHistory: Leave[] = [];
  userId: string | null = null;

  constructor(private readonly leaveApi: LeaveApiService, private readonly auth: AuthService) {}

  ngOnInit(): void {
    this.auth.user$.subscribe((user) => {
      if (user) {
        this.userId = user.id.toString();
        this.loadLeaveHistory();
      }
    });
  }

  loadLeaveHistory(): void {
    if (this.userId) {
      this.leaveApi.getLeaveHistory(this.userId!).subscribe({
        next: (leaves: Leave[]) => {
          this.leaveHistory = leaves;
        },
        error: (err: any) => console.error('Error fetching leaves\n' + err),
      });
    }
  }
}
