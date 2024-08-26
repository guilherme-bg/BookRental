// src/app/services/auth.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = 'authToken';
  private apiUrl = 'https://localhost:4200';

  constructor(private http: HttpClient, private router: Router, private jwtHelper: JwtHelperService) {}

  login(username: string, password: string) {
    return this.http.post(`${this.apiUrl}/api/Authentication/login`, { username, password })
      .subscribe((response: any) => {
        localStorage.setItem(this.tokenKey, response.token);
        this.router.navigate(['books']);
      });
  }

  register(user: any) {
    return this.http.post(`${this.apiUrl}/api/Authentication/register`, user);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    return token != null && !this.jwtHelper.isTokenExpired(token);
  }
}
