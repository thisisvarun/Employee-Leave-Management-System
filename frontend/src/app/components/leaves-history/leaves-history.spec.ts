import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeavesHistory } from './leaves-history';

describe('LeavesHistory', () => {
  let component: LeavesHistory;
  let fixture: ComponentFixture<LeavesHistory>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeavesHistory]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeavesHistory);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
