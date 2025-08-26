import { Component, Inject, OnInit } from '@angular/core';
import { UserService, UserSaveDto, UserDto } from '../Services/user.service';
import { forkJoin } from 'rxjs';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-add-user',
  templateUrl: './addUser.component.html',
  styleUrls: ['./dialog.css'],
})
export class AddUserComponent implements OnInit {
  depts: any[] = [];
  groups: any[] = [];
  selectedGroupId: number | null = null;
  selectedGroups: any[] = [];

  user: UserSaveDto = {
    userName: '',
    empName: '',
    natId: '',
    deptId: null,
    extClctr: false,
    stopped: false,
    workGroupIds: [],
  };
  // editableUser: AddUserDto | null = null;

  constructor(
    private userService: UserService,
    @Inject(MAT_DIALOG_DATA)
    public data: { user: UserDto | null; title: string }
  ) {}

  ngOnInit(): void {
    forkJoin({
      depts: this.userService.getDepts(),
      groups: this.userService.getGroups(),
    }).subscribe(({ depts, groups }) => {
      this.depts = depts;
      this.groups = groups;
      if (this.data.user) {
        const { id, deptName, workGroups, ...userToSave } = this.data.user;
        this.user = userToSave;
        console.log('Editing user with ID:', this.user);
        if (this.user.workGroupIds?.length) {
          this.selectedGroups = this.groups.filter((g) =>
            this.user.workGroupIds.includes(g.id)
          );
        }
      }
    });
  }

  addGroup(): void {
    const id =
      this.selectedGroupId != null ? Number(this.selectedGroupId) : NaN;
    if (!id || Number.isNaN(id)) return;
    if (!this.user.workGroupIds.includes(id)) {
      this.user.workGroupIds.push(id);
      const g = this.groups.find((g) => g.id === id);
      if (g) this.selectedGroups.push(g);
    }
    this.selectedGroupId = null;
  }

  removeGroup(id: number): void {
    this.user.workGroupIds = this.user.workGroupIds.filter(
      (x: number) => x !== id
    );
    this.selectedGroups = this.selectedGroups.filter((g: any) => g.id !== id);
  }
}
