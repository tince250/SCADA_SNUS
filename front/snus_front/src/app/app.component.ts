import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'SCADA manager';
  loggedIn = false;

  constructor(private authService: AuthService) {
    this.authService.recieveLoggedIn().subscribe({
      next: (value) => {
        this.loggedIn = value;
        if (!this.loggedIn) {
          this.loggedIn = this.authService.getUserFromStorage() == null? false: true;
        }
      },
      error: (err) => {
        console.log("Error getting current logged in information.")
      },
    })
  }
}
