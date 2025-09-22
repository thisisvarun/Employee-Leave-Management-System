import { DatePipe } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-team-leave-apply-item',
  imports: [DatePipe],
  templateUrl: './team-leave-apply-item.html',
  styleUrl: './team-leave-apply-item.css',
})
export class TeamLeaveApplyItem {
  @Input() leaveItem: any;
  acceptButtonClicked: boolean = false;
  rejectButtonClicked: boolean = false;
  isSubmitted: boolean = false;
}
