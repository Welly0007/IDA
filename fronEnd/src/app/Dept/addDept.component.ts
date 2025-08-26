import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-add-dept',
  template: `<div class="dialog-form">
    <h2 mat-dialog-title>إضافة إدارة جديدة</h2>
    <mat-dialog-content>
      <form>
        <mat-form-field>
          <mat-label>اسم الإدارة</mat-label>
          <input matInput [(ngModel)]="data.name" name="name" required />
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>إلغاء</button>
      <button mat-button [mat-dialog-close]="{ name: data.name }">إضافة</button>
    </mat-dialog-actions>
  </div>`,
  styleUrls: ['./dialog.css'],
})
export class AddDeptComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: { name: string }) {}
}
