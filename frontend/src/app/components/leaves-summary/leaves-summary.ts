import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { LeaveApiService } from '../../core/services/api/leave-api.service';
import { AuthService } from '../../core/services/auth/auth';
import { CommonModule } from '@angular/common';
import { AgPolarAxisOptions, AgPolarChartOptions } from 'ag-charts-community';
import { LeaveSummaryChart } from '../leave-summary-chart/leave-summary-chart';

@Component({
  selector: 'app-leaves-summary',
  standalone: true,
  imports: [CommonModule, LeaveSummaryChart],
  templateUrl: './leaves-summary.html',
  styleUrl: './leaves-summary.css',
})
export class LeavesSummary implements OnInit {
  public leaveSummary: any = {};
  options!: AgPolarChartOptions;

  constructor(private readonly leaveApi: LeaveApiService, private readonly auth: AuthService) {}

  data = [
    { asset: 'Remaining', amount: 30 },
    { asset: 'Pending', amount: 55 },
    { asset: 'Used', amount: 90 },
  ];

  ngOnInit(): void {
    this.auth.user$.subscribe((user) => {
      if (user) {
        this.leaveApi.getLeaveSummary(user.id.toString()).subscribe((summary) => {
          this.leaveSummary = summary;
          // console.log('STRUCTURE', this.leaveSummary);
        });
      }
    });
  }
}
