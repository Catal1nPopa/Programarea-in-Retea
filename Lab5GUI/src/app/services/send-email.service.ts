import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EmailParameters } from '../models/email-parameters';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SendEmailService {

  private apiUrl = 'https://localhost:7260/Email/SendEmailsv2';

  constructor(private http: HttpClient) { }

  sendEmail(emailParams: EmailParameters): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.apiUrl, emailParams, httpOptions);
  }
}
