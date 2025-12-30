import { Component, inject,ChangeDetectorRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { finalize } from 'rxjs/internal/operators/finalize';
import { AuthService } from '../../core/services/auth.service';


@Component({
  standalone: true,
  selector: 'app-login',
  imports: [FormsModule,CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
 private auth = inject(AuthService);
  private router = inject(Router);
  private http = inject(HttpClient);
   private cdr = inject(ChangeDetectorRef); 

  email = '';
  password = '';
 loading = false;
 error = '';

onSubmit() {
  this.error = '';
  this.loading = true;
this.auth.loginAndLoadProfile(this.email, this.password)
  .pipe(finalize(() => this.loading = false))
  .subscribe({
    next: () => {
      this.router.navigateByUrl('/voting', { replaceUrl: true });
    },
    error: err => {
      this.error =
        err.status === 401 ? 'Invalid email or password'
        : err.status === 0 ? 'Server not reachable'
        : 'Login failed';
    }
  });
}


onBack() {
  // navigate back
}

onSignup() {
  // navigate to signup
}
}

