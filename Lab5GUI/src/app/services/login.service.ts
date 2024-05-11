import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private apiUrl = 'https://localhost:7260/Email/Login'; // Schimbă adresa API-ului cu adresa reală

  constructor(private http: HttpClient) { }

  login(credentials: { email: string, password: string }): Observable<boolean> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<boolean>(this.apiUrl, credentials);
  }
}
