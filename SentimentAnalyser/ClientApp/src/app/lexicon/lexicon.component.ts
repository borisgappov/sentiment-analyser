import { Component, OnInit } from '@angular/core';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { SentimentRating } from '../../generated/api-client/model/sentimentRating';
import { Tools } from '../utils';
import { AntiForgeryTokenService } from 'src/generated/api-client';
import { PlatformLocation } from '@angular/common';

@Component({
  selector: 'app-lexicon',
  templateUrl: './lexicon.component.html',
  styleUrls: ['./lexicon.component.css']
})
export class LexiconComponent implements OnInit {

  dataSource: any;
  url = '/api/Words';
  _window = window;
  sentimentRatings = Tools.EnumToArray(SentimentRating);
  antiForgeryToken = '';

  constructor(
    private antiForgeryTokenService: AntiForgeryTokenService,
    private platformLocation: PlatformLocation
  ) {
    antiForgeryTokenService.configuration.basePath = (platformLocation as any).location.origin;
  }

  ngOnInit(): void {

    this.antiForgeryTokenService
      .apiAntiForgeryTokenGet()
      .subscribe(x => this.antiForgeryToken = x);

    this.dataSource = AspNetData.createStore({
      key: 'id',
      loadUrl: this.url,
      insertUrl: this.url,
      updateUrl: this.url,
      deleteUrl: this.url,
      onBeforeSend: (method, ajaxOptions) => {
        ajaxOptions.xhrFields = { withCredentials: true };
        ajaxOptions.headers = { 'RequestVerificationToken': this.antiForgeryToken };
        if (ajaxOptions.data.values) {
          const values = JSON.parse(ajaxOptions.data.values);
          if (values.sentiment !== undefined) {
            values.sentiment = values.sentiment / 10;
          }
          ajaxOptions.data.values = JSON.stringify(values);
        }
      }
    });
  }

}
