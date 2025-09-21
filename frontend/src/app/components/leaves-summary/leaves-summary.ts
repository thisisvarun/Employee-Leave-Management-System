import { Component, OnInit } from '@angular/core';
import { LeaveApiService } from '../../core/services/api/leave-api.service';
import { AuthService } from '../../core/services/auth/auth';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-leaves-summary',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './leaves-summary.html',
  styleUrl: './leaves-summary.css',
})
export class LeavesSummary implements OnInit {
  leaveSummary: any = {};

  constructor(
    private readonly leaveApi: LeaveApiService,
    private readonly auth: AuthService
  ) {}

  ngOnInit(): void {
    this.auth.user$.subscribe((user) => {
      if (user) {
        this.leaveApi.getLeaveSummary(user.id.toString()).subscribe((summary) => {
          this.leaveSummary = summary;
        });
      }
    });
  }
}
