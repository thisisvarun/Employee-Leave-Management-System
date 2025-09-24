import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllLeaveRequests } from './all-leave-requests';

describe('AllLeaveRequests', () => {
  let component: AllLeaveRequests;
  let fixture: ComponentFixture<AllLeaveRequests>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AllLeaveRequests]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllLeaveRequests);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
