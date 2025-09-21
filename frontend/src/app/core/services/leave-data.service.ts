import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LeaveDataService {
  private leaveSummaryRefresh = new Subject<void>();

  leaveSummaryRefresh$ = this.leaveSummaryRefresh.asObservable();

  refreshLeaveSummary() {
    this.leaveSummaryRefresh.next();
  }
}
