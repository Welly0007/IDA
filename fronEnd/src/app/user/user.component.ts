import { Component, OnInit } from '@angular/core';
import { OrderBy, UserDto, UserService } from '../Services/user.service';
import { AddUserComponent } from './addUser.component';
import { MatDialog } from '@angular/material/dialog';
import { PassDialogComponent } from './passDialog.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['../shared/shared-table.css'], // Use shared CSS
})
export class UserComponent implements OnInit {
  users: UserDto[] = [];
  ascending = false;
  pageNumber: number = 1;
  pageSize: number = 10;
  totalUsers: number = 0;
  searchQuery: string = '';
  OrderBy: OrderBy = OrderBy.Id;

  constructor(
    private userService: UserService,
    private matDialog: MatDialog,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getUsers();
  }
  getUsers() {
    this.userService
      .getUsers(this.pageNumber, this.pageSize, this.ascending, this.OrderBy)
      .subscribe(
        (result) => {
          this.users = result.items;
          this.totalUsers = result.totalCount;
        },
        () => {
          this.toastr.error('تعذر جلب المستخدمين.');
        }
      );
  }
  filterUsers() {
    if (this.searchQuery.trim() === '') {
      this.getUsers(); // If search query is empty, fetch all users
      return;
    }
    this.userService.searchUsers(this.searchQuery).subscribe(
      (filteredUsers) => {
        this.users = filteredUsers.items;
        this.totalUsers = filteredUsers.totalCount;
      },
      () => {
        this.toastr.error('تعذر تصفية المستخدمين.');
      }
    );
  }
  refreshUsers() {
    this.pageNumber = 1;
    this.OrderBy = OrderBy.Id;
    this.pageSize = 10;
    this.ascending = false;
    this.getUsers();
  }

  addUser() {
    const title = 'إضافة موظف جديد';
    const dialogRef = this.matDialog.open(AddUserComponent, {
      data: {
        user: null,
        title: title,
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (!result || !result.user) {
        return;
      }
      this.userService.addUser(result.user).subscribe(
        (response) => {
          this.toastr.success('تم إضافة المستخدم بنجاح.');
          this.getUsers();
        },
        (error) => {
          this.toastr.error('تعذر إضافة المستخدم.');
        }
      );
    });
  }

  editUser(user: UserDto) {
    const dialogRef = this.matDialog.open(AddUserComponent, {
      data: {
        user: user,
        title: 'تعديل موظف',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (!result || !result.user) {
        return; // User cancelled the dialog
      }
      console.log('Editing a7a with ID:', user.id, 'Data:', result.user);
      this.userService.editUser(user.id, result.user).subscribe(
        (response) => {
          this.toastr.success('تم تعديل المستخدم بنجاح.');
          this.getUsers(); // Refresh the user list
        },
        (error) => {
          console.log(result.user);
          this.toastr.error('تعذر تعديل المستخدم.');
          console.error('Error editing user:', error);
        }
      );
    });
  }
  editPassword(id: number, userName: string) {
    const dialogRef = this.matDialog.open(PassDialogComponent, {
      data: {
        id: id,
        userName: userName,
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (!result) {
        return;
      }
      this.userService
        .editPassword(result.userId, result.passwords, result.reset)
        .subscribe(
          (Response) => {
            this.toastr.success('تم تغيير كلمة المرور بنجاح.');
          },
          (error) => {
            this.toastr.error('تعذر تغيير كلمة المرور.');
          }
        );
    });
  }

  onPageChange($event: number) {
    this.pageNumber = $event;
    this.getUsers();
  }
  onPageSizeChange($event: number) {
    this.pageSize = $event;
    this.pageNumber = 1; // Reset to first page
    this.getUsers();
  }
  sortUsers(orderBy: OrderBy) {
    console.log(orderBy);
    this.ascending = !this.ascending;
    this.OrderBy = orderBy;
    this.userService
      .getUsers(this.pageNumber, this.pageSize, this.ascending, this.OrderBy)
      .subscribe(
        (result) => {
          this.users = result.items;
          this.totalUsers = result.totalCount;
        },
        () => {
          this.toastr.error('تعذر جلب المستخدمين.');
        }
      );
  }
}
