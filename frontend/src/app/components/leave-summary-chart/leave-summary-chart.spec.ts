import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveSummaryChart } from './leave-summary-chart';

describe('LeaveSummaryChart', () => {
  let component: LeaveSummaryChart;
  let fixture: ComponentFixture<LeaveSummaryChart>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeaveSummaryChart]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeaveSummaryChart);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
