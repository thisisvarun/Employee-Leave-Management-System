import { Component, inject, OnInit, Renderer2, signal, ViewEncapsulation } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Header } from './header/header';
// import { DarkModeService } from './core/services/dark-mode-service/darkmode.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Header],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('frontend');
  // private readonly darkModeService = inject(DarkModeService);

  // constructor(private readonly darkModeService: DarkModeService) {}

  // ngOnInit() {
  //   this.darkModeService.initTheme();
  //   console.log(this.darkModeService.getCurrentTheme());
  // }

  // getCurrentTheme(): 'light' | 'dark' {
  //   return this.darkModeService.getCurrentTheme();
  // }
}
