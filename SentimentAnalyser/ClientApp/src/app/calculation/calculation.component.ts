import { Component, OnInit, ViewChild } from '@angular/core';
import { DxFileUploaderComponent } from 'devextreme-angular';
import { CalculationsService, AnalyzeTextRequest } from '../../generated/api-client';
import { PlatformLocation } from '@angular/common';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-calculation',
  templateUrl: './calculation.component.html',
  styleUrls: ['./calculation.component.css']
})
export class CalculationComponent implements OnInit {
  @ViewChild('fileUploader') fileUploader: DxFileUploaderComponent;
  textPopupVisible = false;
  filePopupVisible = false;
  textAreaValue = '';
  uploadUrl = '/Calculations/AnalyzeFile';
  uploading = false;
  resultHtml: SafeHtml;

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
  }

  clear() {
    this.resultHtml = null;
  }

  submit() {
    this.calcService.calculationsAnalyzeTextPost({
      text: this.textAreaValue
    } as AnalyzeTextRequest)
      .subscribe(x => {
        this.resultHtml = this.sanitizer.bypassSecurityTrustHtml(x);
        this.textPopupVisible = false;
      });
    this.textAreaValue = '';
  }

  openFileDialog() {
    this.textPopupVisible = true;
  }

  onUploadStarted() {
    this.uploading = true;
  }

  onUploaded(e) {
    var result = JSON.parse(e.request.responseText);
    this.uploading = false;
    this.fileUploader.instance.reset();
    this.filePopupVisible = true;
  }

  onUploadError(e) {
    this.uploading = false;
  }

  onUploadAborted() {
    this.uploading = false;
  }
}
