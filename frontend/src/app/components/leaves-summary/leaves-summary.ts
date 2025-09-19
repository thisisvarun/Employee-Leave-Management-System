import { Component } from '@angular/core';

@Component({
  selector: 'app-leaves-summary',
  imports: [],
  templateUrl: './leaves-summary.html',
  styleUrl: './leaves-summary.css',
})
export class LeavesSummary {
  leaveSummary = {
    total: 16,
    taken: 10,
    absent: 2,
    request: 0,
    workedDays: 240,
    lossOfPay: 2,
  };
}
