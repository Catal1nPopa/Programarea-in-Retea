import { Component, OnInit } from '@angular/core';
import { GetPop3Service } from '../../services/get-pop3.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-get-pop3',
  templateUrl: './get-pop3.component.html',
  styleUrl: './get-pop3.component.css',
})
export class GetPop3Component implements OnInit {
  apiData: any[] | null = null;
  loading = true;

  constructor(private apiService: GetPop3Service, private sanitizer: DomSanitizer) {}

  ngOnInit(): void {
    this.apiService.getApiData().subscribe(
      (data: any[]) => {
        this.apiData = data;
        this.apiData.forEach((email) => {
          if (email.attachment && email.attachmentName) {
            email.isImage =
              email.attachmentName.toLowerCase().endsWith('.png') ||
              email.attachmentName.toLowerCase().endsWith('.jpg') ||
              email.attachmentName.toLowerCase().endsWith('.jpeg') ||
              email.attachmentName.toLowerCase().endsWith('.gif');
          }
          if (email.body) {
            email.safeBody = this.sanitizer.bypassSecurityTrustHtml(email.body);
          }
        });
        this.loading = false;
      },
      (error) => {
        console.error('Eroare la afișarea emailurilor:', error);
        this.loading = false;
      }
    );
  }

  getImage(imageBase64: string): string {
    return 'data:image/png;base64,' + imageBase64;
  }

  downloadAttachment(attachment: string, attachmentName: string): void {
    const byteCharacters = atob(attachment);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
      byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], { type: 'application/octet-stream' });

    const url = window.URL.createObjectURL(blob);
    const filename = `attachment.${attachmentName}`;

     const anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = filename;
    document.body.appendChild(anchor);
    anchor.click();

    window.URL.revokeObjectURL(url);
    document.body.removeChild(anchor);
  }

  toggleEmailDetails(email: any) {
    email.showDetails = !email.showDetails;
  }
}
