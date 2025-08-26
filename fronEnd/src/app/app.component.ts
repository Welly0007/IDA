import { Component } from '@angular/core';
import { UiService } from './Services/ui.service';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  isNavOpen = false;
  private navSubscription!: Subscription;

  constructor(private uiService: UiService) {
    this.navSubscription = this.uiService.navOpen$.subscribe((isOpen) => {
      this.isNavOpen = isOpen;
    });
  }
  ngOnDestroy(): void {
    // 5. IMPORTANT: Unsubscribe to prevent memory leaks
    if (this.navSubscription) {
      this.navSubscription.unsubscribe();
    }
  }
}
