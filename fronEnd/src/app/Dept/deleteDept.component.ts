import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-deleteDept',
  template: `<div class="dialog-form">
    <h2 mat-dialog-title>حذف الإدارة</h2>
    <mat-dialog-content>
      <p>هل أنت متأكد أنك تريد حذف الإدارة "{{ data.name }}"؟</p>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>إلغاء</button>
      <button mat-button [mat-dialog-close]="true">حذف</button>
    </mat-dialog-actions>
  </div>`,
  styleUrls: ['./dialog.css'],
})
export class deleteDeptComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { id: number; name: string }
  ) {}
}
