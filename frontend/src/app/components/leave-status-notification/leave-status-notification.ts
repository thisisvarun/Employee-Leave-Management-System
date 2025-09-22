import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeaveApiService } from '../../core/services/api/leave-api.service';
import { AuthService } from '../../core/services/auth/auth';
import { ZardAlertComponent } from '@shared/components/alert/alert.component';

@Component({
  selector: 'app-leave-status-notification',
  standalone: true,
  imports: [CommonModule, ZardAlertComponent],
  templateUrl: './leave-status-notification.html',
  styleUrl: './leave-status-notification.css',
})
export class LeaveStatusNotificationComponent implements OnInit {
  recentLeave: any | null = null;
  showNotification: boolean = false;
  title = 'Leave Accepted alert!';

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
            console.error('You dont seem to take leaves that often, Good!!');
          },
        });
      }
    });
  }

  dismissNotification(): void {
    this.showNotification = false;
  }
}
