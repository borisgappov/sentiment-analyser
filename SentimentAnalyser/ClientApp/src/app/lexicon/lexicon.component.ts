import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-lexicon',
  templateUrl: './lexicon.component.html',
  styleUrls: ['./lexicon.component.css']
})
export class LexiconComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    console.log('-- lexicon --')
  }

}
