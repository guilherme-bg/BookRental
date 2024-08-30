import { Component } from '@angular/core';
import { AuthService } from '../services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {
  credentials = {
    username: '',
    password: ''
  };

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.authService.login(this.credentials).subscribe(
      (response: string) => {
        localStorage.setItem('jwt', response);
        console.log('Login succeeded')
        this.router.navigate(['/books']);
      },
      error => {
        console.error('Login failed', error);
      }
    );
  }
}
