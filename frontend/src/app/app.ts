import { Component, inject, OnInit, Renderer2, signal, ViewEncapsulation } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Header } from './header/header';
import { AgCharts } from 'ag-charts-angular';
import { AgPolarChartOptions } from 'ag-charts-community';
import { ZardCardComponent } from '@shared/components/card/card.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Header],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('frontend');
  public options;

  data = [
    { asset: 'Total', amount: 10 },
    { asset: 'Used', amount: 3 },
    { asset: 'Pending', amount: 2 },
  ];

  constructor() {
    this.options = {
      data: this.data,
      title: {
        text: 'Leaves Summary',
        color: '#fff',
        fontSize: 26,
      },
      series: [
        {
          type: 'donut',
          calloutLabelKey: 'asset',
          angleKey: 'amount',
          calloutLabel: {
            color: 'white',
            fontSize: 22,
          },
        },
      ],
      padding: { top: 10, right: 10, bottom: 10, left: 10 },
      legend: {
        item: {
          label: {
            color: 'white',
            fontSize: 16,
          },
        },
      },
      background: {
        fill: 'oklch(0.21 0.006 285.885)',
      },
    } as AgPolarChartOptions;
  }
}
