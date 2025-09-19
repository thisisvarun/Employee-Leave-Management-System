import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeavesSummary } from './leaves-summary';

describe('LeavesSummary', () => {
  let component: LeavesSummary;
  let fixture: ComponentFixture<LeavesSummary>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeavesSummary]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeavesSummary);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
