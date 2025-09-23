import { Component, OnInit } from '@angular/core';
import { LeaveApiService } from '../../core/services/api/leave-api.service';
import { AuthService } from '../../core/services/auth/auth';
import { CommonModule } from '@angular/common';
import { Leave } from '@shared/models/Leave';

// export interface LeaveDate {
//   hours: number;
//   date: string;
// }

// export interface Leave {
//   employeeId: number;
//   leaveType: 'Casual' | 'Sick' | 'Annual' | 'LIEU';
//   description: string;
//   dates: LeaveDate[];
// }   ->moved to models/Leave

@Component({
  selector: 'app-leaves-history',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './leaves-history.html',
  styleUrls: ['./leaves-history.css']
})
export class LeavesHistory implements OnInit {
  leaveHistory: Leave[] = [];

  constructor(
    private readonly leaveApi: LeaveApiService,
    private readonly auth: AuthService
  ) {}

  ngOnInit(): void {
    this.auth.user$.subscribe(user => {
      if (user) {
        this.leaveApi.getLeaveHistory(user.id.toString())
          .subscribe((history) => {
            this.leaveHistory = history as Leave[]; // casting the response
          });
      }
    });
  }

  trackByRequestId(index: number, leave: Leave) {
    return leave.requestId;
  }
}
