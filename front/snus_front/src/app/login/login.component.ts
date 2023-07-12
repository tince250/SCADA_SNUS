import { CredentialsDTO } from './../services/auth.service';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  isVisible: boolean = false;

  loginForm = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required])
  });

  constructor(private authService: AuthService,
    public snackBar: MatSnackBar,
    private router: Router,) {
  }

  ngOnInit(): void {
  }

  login(): void {
    if (this.loginForm.valid) {
      let creds: CredentialsDTO = {
        username: this.loginForm.value.username!,
        password: this.loginForm.value.password!
      }

      this.authService.login(creds).subscribe({
        next: (value) => {
          console.log("succ\n" + JSON.stringify(value));
          localStorage.setItem("user", JSON.stringify(value));
          this.authService.setUser();
          this.authService.setLoggedIn(true);
          this.router.navigate([""]);
        },
        error: (err) => {
          this.snackBar.open(err.error, "", {
            duration: 2700, panelClass: ['snack-bar-server-error']
         });
        }
      })
    }
  }
}
