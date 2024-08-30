import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authUrl = 'https://localhost:7236/api/Authentication';

  constructor(private http: HttpClient, public jwtHelper: JwtHelperService) {}

  login(credentials: any): Observable<string> {    
    return this.http.post(`${this.authUrl}/login`, credentials, { responseType: 'text' });
  }

  register(credentials: any): Observable<any> {
    return this.http.post(`${this.authUrl}/register`, credentials, { responseType: 'text' });
  } 

  logout() {
    localStorage.removeItem('jwt');
  }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem('jwt');
    return !this.jwtHelper.isTokenExpired(token);
  }
}
