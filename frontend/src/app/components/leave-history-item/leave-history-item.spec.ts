import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveHistoryItem } from './leave-history-item';

describe('LeaveHistoryItem', () => {
  let component: LeaveHistoryItem;
  let fixture: ComponentFixture<LeaveHistoryItem>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeaveHistoryItem]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeaveHistoryItem);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
