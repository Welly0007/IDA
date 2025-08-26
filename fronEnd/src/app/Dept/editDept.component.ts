import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-editDept',
  template: `<div class="dialog-form">
    <h2 mat-dialog-title>تعديل الإدارة</h2>
    <form #editDeptForm="ngForm">
      <mat-form-field appearance="fill">
        <mat-label>اسم الإدارة</mat-label>
        <input matInput [(ngModel)]="data.name" name="name" required />
      </mat-form-field>
      <!-- Add more fields as needed -->
      <mat-dialog-actions align="end">
        <button mat-button mat-dialog-close>إلغاء</button>
        <button
          mat-button
          [mat-dialog-close]="data"
          [disabled]="!editDeptForm.form.valid"
        >
          حفظ
        </button>
      </mat-dialog-actions>
    </form>
  </div>`,
  styleUrls: ['./dialog.css'],
})
export class EditDeptComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { id: number; name: string }
  ) {}
  // Component logic goes here
}
