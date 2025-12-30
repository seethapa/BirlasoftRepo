import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { switchMap, tap } from 'rxjs';

interface AuthResponse {
  token: string;
  expiresAt: string;
  email: string;
  role: string;
  fullName: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private currentUser: any;
  private http = inject(HttpClient);
  // private apiUrl = 'https://localhost:7216/api/auth'; // adjust port
  private apiBase = 'https://localhost:7216/api';

  login(email: string, password: string) {
    return this.http.post<AuthResponse>(`${this.apiBase}/auth/login`, { email, password })
      .pipe(
        tap(res => {
          localStorage.setItem('token', res.token);
        })
      );
  }

  loginAndLoadProfile(email: string, password: string) {
  return this.login(email, password).pipe(
    switchMap(() => this.http.get(`${this.apiBase}/auth/me`)),
    tap(user => this.setUser(user))
  );
}

  register(data: { email: string; password: string; firstName: string; lastName: string }) {
    return this.http.post<AuthResponse>(`${this.apiBase}/auth/register`, data);
  }

  logout() {
    localStorage.removeItem('token');
  }

  getToken() {
    return localStorage.getItem('token');
  }

  isLoggedIn() {
    return !!this.getToken();
  }
  setUser(user: any) {
  this.currentUser = user;
}

getUser() {
  return this.currentUser;
}

}
