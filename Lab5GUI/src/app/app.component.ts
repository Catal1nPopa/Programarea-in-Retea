import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'EmailGUI';
  visible:boolean = false;
  visibleIMAP: boolean = false;
  visiblePOP3: boolean = false;
  loginSuccessful: boolean = false;

  onClick()
  {
    this.visible = !this.visible
  }

  onClickIMAP()
  {
    this.visibleIMAP = !this.visibleIMAP
  }

  onClickPOP3()
  {
    this.visiblePOP3 = !this.visiblePOP3
  }

  onLoginSuccess(success: boolean) {
    this.loginSuccessful = success;
  }
}
