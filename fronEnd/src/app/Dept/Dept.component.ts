import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EditDeptComponent } from './editDept.component';
import { deleteDeptComponent } from './deleteDept.component';
import { AddDeptComponent } from './addDept.component';
import { DeptDto, DeptService } from '../Services/dept.service';
import { ToastrService } from 'ngx-toastr';
import { error } from 'console';

@Component({
  selector: 'app-Dept',
  templateUrl: './Dept.component.html',
  styleUrls: ['../shared/shared-table.css', '../shared/toast.css'],
})
export class DeptComponent implements OnInit {
  depts: DeptDto[] = [];
  totalDepts: number = 0;
  ascending: boolean = true;
  searchTerm: string = '';
  pageNumber: number = 1;
  pageSize: number = 10;

  constructor(
    private dialog: MatDialog,
    private deptService: DeptService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getDepts();
  }

  onPageChange(pageNumber: number): void {
    this.pageNumber = pageNumber;
    this.getDepts();
  }
  onPageSizeChange(pageSize: number): void {
    this.pageSize = pageSize;
    this.pageNumber = 1;
    this.getDepts();
  }
  sortDepts(): void {
    this.ascending = !this.ascending;
    this.getDepts();
  }

  getDepts() {
    this.deptService
      .getDepts(this.pageNumber, this.pageSize, this.ascending)
      .subscribe(
        (result) => {
          this.depts = result.items;
          this.totalDepts = result.totalCount;
        },
        () => {
          this.toastr.error('تعذر جلب الإدارات.');
        }
      );
  }
  editDept(dept: DeptDto): void {
    const dialogRef = this.dialog.open(EditDeptComponent, {
      data: { ...dept },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const idx = this.depts.findIndex((d) => d.id === result.id);
        if (idx !== -1) {
          this.depts[idx] = result;
          this.deptService.updateDept(result).subscribe({
            next: () => {
              this.toastr.success('تم تحديث الإدارة بنجاح.');
            },
            error: (err) => {
              this.toastr.error('تعذر تحديث الإدارة.');
            },
          });
        }
      }
    });
  }
  deleteDept(dept: DeptDto): void {
    const dialogRef = this.dialog.open(deleteDeptComponent, {
      data: { ...dept },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deptService.deleteDept(dept.id).subscribe({
          next: () => {
            this.toastr.success('تم حذف الإدارة بنجاح.');
            this.getDepts();
          },
          error: (err) => {
            // Show error message if deletion fails (e.g., foreign key constraint)
            this.toastr.error(
              'قد تكون مرتبطة ببيانات أخرى.',
              'تعذر حذف الإدارة'
            );
          },
        });
      }
    });
    // this.getDepts(this.pageNumber, this.pageSize);
  }
  addDept(): void {
    const dialogRef = this.dialog.open(AddDeptComponent, {
      data: { name: '' },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deptService.addDept(result).subscribe({
          next: () => {
            this.toastr.success('تم إضافة الإدارة بنجاح.');
            // Refresh the departments list after successful addition
            this.getDepts();
          },
          error: (err) => {
            this.toastr.error('تعذر إضافة الإدارة.');
          },
        });
      } else {
        this.toastr.info('تم إغلاق النافذة بدون إضافة إدارة جديدة.');
      }
    });
  }
  filterDepts(): void {
    if (this.searchTerm) {
      this.deptService.searchDepts(this.searchTerm).subscribe(
        (filteredDepts) => {
          this.depts = filteredDepts;
          this.totalDepts = filteredDepts.length;
        },
        (error) => {
          this.toastr.error('تعذر تصفية الإدارات.');
        }
      );
    } else {
      this.getDepts();
    }
  }
}
