import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {
  constructor(private readonly router: Router) {}

  handleLoginButtonClicke() {
    this.router.navigate(['/login']);
    console.log('heading to login page!');
  }
}
