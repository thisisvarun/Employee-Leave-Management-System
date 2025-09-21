import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeaveApiService } from '../../core/services/api/leave-api.service';
import { AuthService } from '../../core/services/auth/auth';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-leave-status-notification',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './leave-status-notification.html',
  styleUrl: './leave-status-notification.css',
})
export class LeaveStatusNotificationComponent implements OnInit {
  recentLeave: any | null = null;
  showNotification: boolean = false;

  constructor(private readonly leaveApi: LeaveApiService, private readonly auth: AuthService) {}

  ngOnInit(): void {
    this.auth.user$.subscribe((user) => {
      if (user) {
        this.leaveApi.getRecentLeaveStatus(user.id.toString()).subscribe({
          next: (leave) => {
            console.log('LEAVE', leave);
            if (leave) {
              this.recentLeave = leave;
              this.showNotification = true;
            }
          },
          error: (err) => {
            console.error('Error fetching recent leave status:', err);
          },
        });
      }
    });
  }

  dismissNotification(): void {
    this.showNotification = false;
  }
}
