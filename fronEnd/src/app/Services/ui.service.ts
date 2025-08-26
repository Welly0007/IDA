import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UiService {
  private navOpen = new BehaviorSubject<boolean>(true);
  navOpen$ = this.navOpen.asObservable();
  constructor() {}
  toggleNav(): void {
    this.navOpen.next(!this.navOpen.value);
  }
}
