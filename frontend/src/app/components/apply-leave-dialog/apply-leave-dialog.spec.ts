import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplyLeaveDialog } from './apply-leave-dialog';

describe('ApplyLeaveDialog', () => {
  let component: ApplyLeaveDialog;
  let fixture: ComponentFixture<ApplyLeaveDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApplyLeaveDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApplyLeaveDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
