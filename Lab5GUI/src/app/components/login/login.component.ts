import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { LoginService } from '../../services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  username: string = '';
  password: string = '';
  @Output() loginSuccess = new EventEmitter<boolean>();

  constructor(private authService: LoginService, private router: Router) { }
  ngOnInit(): void {
  }

  login(): void {
    console.log(this.username)
    this.authService.login({ email: this.username, password: this.password })
      .subscribe(success => {
        if (success) {
          console.log('Autentificare reușită!');
          this.loginSuccess.emit(true)
          this.router.navigate(['/dashboard']); 
        } else {
          console.log('Autentificare eșuată!');
        }
      });
  }
}
