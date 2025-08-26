import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css'],
})
export class PaginationComponent implements OnInit {
  constructor() {}
  private _totalItems: number = 0;

  @Input()
  set totalItems(value: number) {
    this._totalItems = value;
    this.calculateTotalPages();
  }
  get totalItems(): number {
    return this._totalItems;
  }

  pageNumber: number = 1;
  pageSize: number = 10;
  totalPages: number = 0;
  pageSizes = [5, 10, 25, 100];
  pageStart: number = 0;
  pageEnd: number = 0;

  ngOnInit(): void {
    this.calculateTotalPages();
  }

  private calculateTotalPages(): void {
    this.totalPages = Math.max(1, Math.ceil(this.totalItems / this.pageSize));
    this.updatePageInfo();
  }
  private updatePageInfo(): void {
    this.pageStart = (this.pageNumber - 1) * this.pageSize + 1;
    this.pageEnd = Math.max(
      this.pageStart,
      Math.min(this.pageStart + this.pageSize - 1, this.totalItems)
    );
  }

  @Output() pageChange = new EventEmitter<number>();
  @Output() pageSizeChange = new EventEmitter<number>();

  nextPage(): void {
    this.pageChange.emit(++this.pageNumber);
    this.updatePageInfo();
  }

  prevPage(): void {
    this.pageChange.emit(--this.pageNumber);
    this.updatePageInfo();
  }
  onPageSizeChange(newPageSize: number): void {
    this.pageSize = newPageSize;
    this.pageNumber = 1; // Reset to first page
    this.pageSizeChange.emit(newPageSize);
    this.calculateTotalPages();
  }
}
