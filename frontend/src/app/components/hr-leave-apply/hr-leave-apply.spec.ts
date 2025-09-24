import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrLeaveApply } from './hr-leave-apply';

describe('HrLeaveApply', () => {
  let component: HrLeaveApply;
  let fixture: ComponentFixture<HrLeaveApply>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HrLeaveApply]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HrLeaveApply);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
