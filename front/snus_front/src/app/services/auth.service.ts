import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private user$ = new BehaviorSubject<User|null>(null);
  private loggedIn$ = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) { }

  getUser(): User|null {
    return this.user$.value;
  }

  setUser(): void {
    this.user$.next(this.getUserFromStorage());
  }

  setLoggedIn(is: boolean) : void {
    this.loggedIn$.next(is);
  }

  recieveLoggedIn(): Observable<boolean> {
    return this.loggedIn$.asObservable();
  }

  login(creds: CredentialsDTO): Observable<any> {
    return this.http.post<any>(environment.apiHost + '/user/login', creds, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    });
  }

  logout(): void {
    localStorage.removeItem('user');
    this.setUser();
    this.setLoggedIn(false);
  }

  isLoggedIn(): boolean {
    if (localStorage.getItem('user') != null) {
      return true;
    }
    return false;
  }

  getUserFromStorage() : User|null {
    if (this.isLoggedIn()) {
      const storedUser: User|null = JSON.parse(localStorage.getItem('user')!);
      return storedUser;
    }
    return null;
  }

}

export interface CredentialsDTO {
  username: string,
  password: string
}

export interface User {
  id: number,
  email: string,
  role: string,
  name: string,
  lastName: string
}
