import { AuthService } from './../services/auth.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  role: string|null = ''

  constructor(private authService: AuthService) {
    this.role = this.authService.getRole();
  }

  logout() {
    this.authService.logout();
  }
}
