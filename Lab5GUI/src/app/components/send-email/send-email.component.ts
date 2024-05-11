import { Component } from '@angular/core';
import { EmailParameters } from '../../models/email-parameters';
import { SendEmailService } from '../../services/send-email.service';

@Component({
  selector: 'app-send-email',
  templateUrl: './send-email.component.html',
  styleUrls: ['./send-email.component.css'] // Corrected the styleUrl to styleUrls
})
export class SendEmailComponent {

  emailParams: EmailParameters = {
    ToEmail: '',
    ReplyTo: '',
    Subject: '',
    Body: '',
    Attachment: '',
    AttachmentName: ''
  };

  constructor(private emailService: SendEmailService) { }

  onSubmit(): void {
    if (this.emailParams.Attachment) {
      this.convertAttachmentToBase64()
        .then(() => this.sendEmail());
    } else {
      this.sendEmail();
    }
  }

  private async convertAttachmentToBase64(): Promise<void> {
    const attachmentInput = document.getElementById('attachment') as HTMLInputElement;
    if (attachmentInput.files && attachmentInput.files[0]) {
      const file = attachmentInput.files[0];
      const reader = new FileReader();
      reader.readAsDataURL(file);
      return new Promise<void>((resolve, reject) => {
        reader.onload = () => {
          let base64String = reader.result as string;
          if (base64String.startsWith('data:image')) {
            base64String = base64String.replace(/^data:image\/(png|jpg|jpeg|gif);base64,/, '');
          } else if (base64String.startsWith('data:application/pdf')) {
            base64String = base64String.replace(/^data:application\/pdf;base64,/, '');
          } else if (base64String.startsWith('data:application/msword') || base64String.startsWith('data:application/vnd.openxmlformats-officedocument.wordprocessingml.document')) {
            base64String = base64String.replace(/^data:application\/msword;base64,|^data:application\/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,/, '');
          } else if (base64String.startsWith('data:video')) {
            base64String = base64String.replace(/^data:video\/(mp4|mpeg|ogg|webm);base64,/, '');
          }
          this.emailParams.Attachment = base64String;
          this.emailParams.AttachmentName = file.name;
          resolve();
        };
        reader.onerror = (error) => reject(error);
      });
    } else {
      return Promise.reject("No file selected");
    }
  }
  
  
  

  private sendEmail(): void {
    this.emailService.sendEmail(this.emailParams)
      .subscribe(response => {
        console.log('Email sent successfully:', response);
        console.log(this.emailParams);
       this.emailParams = {
          ToEmail: '',
          ReplyTo: '',
          Subject: '',
          Body: '',
          Attachment: '',
          AttachmentName: ''
        };
      }, error => {
        console.error('Failed to send email:', error);
      });
  }
}
