import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { VotingComponent } from './voting/voting.component';




@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, VotingComponent],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {


  user: any;
  private http = inject(HttpClient);
  private auth = inject(AuthService);
constructor(private route: ActivatedRoute) {}
 ngOnInit() {

    if (typeof window === 'undefined') return;


  this.http.get("https://localhost:7216/api/auth/me")
    .subscribe({
      next: res => {
        console.log('PROFILE DATA', res);
        this.user = res;
      },
      error: err => {
        console.error('PROFILE ERROR', err);
      }
    });

    this.user = this.auth.getUser();
    //  this.user = this.route.snapshot.data['user'];
     const user = this.route.snapshot.data['user'];
       // fallback (hard refresh case)
  if (!this.user) {
    this.http.get('https://localhost:7216/api/auth/me')
      .subscribe(user => this.user = user);
  }
    
}

}
