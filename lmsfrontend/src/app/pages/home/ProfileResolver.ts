import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class ProfileResolver {
  private http = inject(HttpClient);

  resolve() {
    return this.http.get('https://localhost:7216/api/users/me');
  }
}
