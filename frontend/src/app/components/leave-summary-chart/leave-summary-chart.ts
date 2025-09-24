import { Component, Input, OnInit, OnChanges, SimpleChange, SimpleChanges } from '@angular/core';
import { AgPolarChartOptions } from 'ag-charts-community';
import { AgCharts } from 'ag-charts-angular';

interface LeaveSummary {
  total: number;
  approved: number;
  pending: number;
}

@Component({
  selector: 'app-leave-summary-chart',
  imports: [AgCharts],
  templateUrl: './leave-summary-chart.html',
  styleUrl: './leave-summary-chart.css',
})
export class LeaveSummaryChart implements OnInit, OnChanges {
  @Input() leaveSummary: any;
  public options!: AgPolarChartOptions;
  public data: unknown;
  @Input() leaveType!: string;

  constructor() {}

  ngOnInit(): void {
    this.prepareChart();
  }

  ngOnChanges(changes: SimpleChanges) {
    this.prepareChart();
  }

  prepareChart() {
    this.data = [
      { total: 'Pending', remaining: this.leaveSummary?.pending },
      {
        total: 'Remaining',
        remaining:
          this.leaveSummary?.total - (this.leaveSummary?.approved + this.leaveSummary?.pending),
      },
      { total: 'Used', remaining: this.leaveSummary?.approved },
    ];

    this.options = {
      data: this.data,
      title: {
        text: `${this.leaveType} Leaves Summary`,
        color: '#fff',
        fontSize: 20,
        fontWeight: 'bold',
      },
      series: [
        {
          type: 'donut',
          calloutLabelKey: 'total',
          angleKey: 'remaining',
          calloutLabel: {
            color: 'white',
            fontSize: 12,
          },
        },
      ],
      padding: { top: 10, right: 10, bottom: 10, left: 10 },
      legend: {
        enabled: false,
      },
      background: {
        fill: 'oklch(0.21 0.006 285.885)',
      },
    } as unknown as AgPolarChartOptions;
  }
}
