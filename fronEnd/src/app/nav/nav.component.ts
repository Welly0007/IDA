import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  isDropdownOpen = false;
  constructor() {}

  ngOnInit(): void {
    // 4. Subscribe to the service's observable
  }
  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
}
