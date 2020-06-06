import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  selectedTabIndex = this.router.url === '/' ? 0 : 1;

  constructor(
    private router: Router,
    private route: ActivatedRoute
  ) {
  }

  onSelectionChanged() {
    if (this.selectedTabIndex === 0) {
      this.router.navigate(['']);
    } else if (this.selectedTabIndex === 1) {
      this.router.navigate(['lexicon'], { relativeTo: this.route });
    }
  }
}
