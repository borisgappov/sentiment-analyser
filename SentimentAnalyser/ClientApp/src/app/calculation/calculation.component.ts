import { Component, OnInit, ViewChild } from '@angular/core';
import { DxFileUploaderComponent, DxTextAreaComponent } from 'devextreme-angular';
import { CalculationsService, AnalyzeTextRequest, AnalyzeTextResponse } from '../../generated/api-client';
import { PlatformLocation } from '@angular/common';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import notify from 'devextreme/ui/notify';

@Component({
  selector: 'app-calculation',
  templateUrl: './calculation.component.html',
  styleUrls: ['./calculation.component.css']
})
export class CalculationComponent implements OnInit {
  @ViewChild('fileUploader') fileUploader: DxFileUploaderComponent;
  @ViewChild('textArea') textArea: DxTextAreaComponent;

  textPopupVisible = false;
  filePopupVisible = false;
  textAreaValue = '';
  uploadUrl = '/api/Calculations/AnalyzeFile';
  uploading = false;
  resultHtml: SafeHtml;
  score = 0;

  constructor(
    private calcService: CalculationsService,
    private platformLocation: PlatformLocation,
    private sanitizer: DomSanitizer
  ) {
    calcService.configuration.basePath = (platformLocation as any).location.origin;
  }

  ngOnInit(): void {
  }

  openTextDialog() {
    this.textPopupVisible = true;
    setTimeout(() => this.textArea.instance.focus(), 500);
  }

  clear() {
    this.resultHtml = null;
    this.score = 0;
  }

  submit() {
    if (this.textAreaValue.trim().length === 0) {
      notify('Please enter text', 'warning', 3000);
    } else {
      this.calcService.apiCalculationsAnalyzeTextPost({
        text: this.textAreaValue
      } as AnalyzeTextRequest)
        .subscribe(x => {
          this.resultHtml = this.sanitizer.bypassSecurityTrustHtml(x.html);
          this.score = x.score;
          this.textPopupVisible = false;
        });
      this.textAreaValue = '';
    }
  }

  openFileDialog() {
    this.filePopupVisible = true;
  }

  onUploadStarted() {
    this.uploading = true;
  }

  onUploaded(e) {
    if (e.request.responseText) {
      const result = JSON.parse(e.request.responseText) as AnalyzeTextResponse;
      this.resultHtml = this.sanitizer.bypassSecurityTrustHtml(result.html);
      this.score = result.score;
      this.filePopupVisible = false;
    }
    this.uploading = false;
    this.fileUploader.instance.reset();
  }

  onUploadError(e) {
    this.uploading = false;
  }

  onUploadAborted() {
    this.uploading = false;
  }
}
