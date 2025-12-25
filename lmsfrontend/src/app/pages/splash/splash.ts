import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-splash',
  imports: [],
  templateUrl: './splash.html',
  styleUrl: './splash.css',
})
export class Splash {
  constructor(private router: Router) {
    setTimeout(() => this.router.navigate(['/login']), 2500);
  }
}
