import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeamLeaveApplyItem } from './team-leave-apply-item';

describe('TeamLeaveApplyItem', () => {
  let component: TeamLeaveApplyItem;
  let fixture: ComponentFixture<TeamLeaveApplyItem>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TeamLeaveApplyItem]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TeamLeaveApplyItem);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
