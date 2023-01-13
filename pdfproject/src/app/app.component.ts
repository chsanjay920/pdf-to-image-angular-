import { Component } from '@angular/core';
import { Input } from 'editable-pdf-in-angular/src/app/input';
import { PDFDocumentProxy } from 'ng2-pdf-viewer';
import { Iinput } from './input';
import { HttpClient } from '@angular/common/http';

import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'pdfproject';
  pdfSrc = '/assets/GT4000a.pdf';
  Output = '';
  FinalPdf: any;
  url: any;
  pdf: any;
  constructor(private http: HttpClient, private sanitizer: DomSanitizer) {}

  public async postPdf(final: any) {
    let blob = new Blob([final], { type: 'application/pdf' });
    let formData = new FormData();
    formData.append('file', blob);
    this.http.post('https://localhost:7011/api/PdfToPng', formData).subscribe(
      (response) => {
        console.log(response);
        var imagedata = 'data:image/png;base64,' + response;
        this.url = this.sanitizer.bypassSecurityTrustUrl(imagedata);
      },
      (error) => console.log(error)
    );
  }
  public async loadComplete(pdf: PDFDocumentProxy): Promise<void> {
    for (let i = 1; i <= pdf.numPages; i++) {
      let currentPage: any;
      var currentPageInputsAnn: any;
      var jsonData: any;
      var dummyarray: Iinput[] = [];
      pdf
        .getPage(i)
        .then((p) => {
          currentPage = p;
          return p.getAnnotations();
        })
        .then((ann) => {
          currentPageInputsAnn = ann;
          console.log(ann);
          currentPageInputsAnn.forEach((element: any) => {
            dummyarray.push({
              feildname: element['fieldName'],
              feildtype: element['fieldType'],
              feildvalue: element['fieldValue'],
            });
          });
          jsonData = JSON.stringify(dummyarray);
          this.Output = jsonData;
        });
      pdf.saveDocument().then((data: any) => {
        this.FinalPdf = data;
      });
      this.pdf = pdf;
    }
  }
  public save() {
    this.pdf.saveDocument().then((data: any) => {
     this.postPdf(data);
    });
  }
}
