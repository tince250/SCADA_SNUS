import { Router } from '@angular/router';
import { AuthService } from './../services/auth.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  role: string|null = ''

  constructor(private authService: AuthService, private router: Router) {
    this.role = this.authService.getRole();
  }

  logout() {
    this.authService.logout();
    this.router.navigate(["login"]);
  }
}
