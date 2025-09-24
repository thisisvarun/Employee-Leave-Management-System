import { DatePipe, JsonPipe } from '@angular/common';
import { Component, EventEmitter, input, Input, Output, SimpleChanges } from '@angular/core';
import { LeaveApiService } from '../../core/services/api/leave-api.service';
import { finalize } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Leave } from '@shared/models/Leave';

@Component({
  selector: 'app-leave-history-item',
  imports: [DatePipe],
  templateUrl: './leave-history-item.html',
  styleUrl: './leave-history-item.css',
})
export class LeaveHistoryItem {
  constructor(private readonly leaveapi: LeaveApiService, private toastr: ToastrService) {}

  @Input() leaveItem: Leave | undefined;
  @Input() index: number | undefined;
  totalTime: number = 0;

  leaveTypeMap: Record<number, string> = {
    0: 'Casual',
    1: 'Sick',
    2: 'Annual',
    3: 'LIEU',
  };

  leaveStatusMap: Record<number, string> = {
    0: 'Accepted',
    1: 'Rejected',
    2: 'Pending',
  };

  ngOnChanges(changes: SimpleChanges) {
    if (changes['leaveItem'] && this.leaveItem?.dates) {
      this.totalTime = this.leaveItem.dates.reduce((sum, d) => sum + d.hours, 0);
      // console.log('Leave status from backend:', this.leaveItem?.leaveStatus);
    }
  }

  getLeaveTypeLabel(type: number | undefined): string {
    if (type === undefined || type === null) return 'Unknown';
    return this.leaveTypeMap[type] ?? 'unknown';
  }

  getLeaveColor(type: number | undefined): string {
    switch (type) {
      case 0:
        return '#4caf50';
      case 1:
        return '#f44336';
      case 2:
        return '#2196f3';
      case 3:
        return '#ff9800';
      default:
        return '#9e9e9e';
    }
  }

  getStatusColor(type: number | undefined): string {
    switch (type) {
      case 0:
        return '#4caf50';
      case 1:
        return '#f44336';
      case 2:
        return '#2196f3';
      default:
        return '#9e9e9e';
    }
  }

  getStatusLabel(type: number | undefined): string {
    if (type === undefined || type === null) return 'Unknown1';
    return this.leaveStatusMap[type] ?? 'Pending';
  }
}
