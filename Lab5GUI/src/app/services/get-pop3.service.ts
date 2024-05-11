import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GetPop3Service {

  private apiURL = "https://localhost:7260/Email/GetEmaiPOP3";
  constructor(private http: HttpClient) { }

  getApiData(): Observable<any> {
    return this.http.get<any>(this.apiURL);
  }
}
