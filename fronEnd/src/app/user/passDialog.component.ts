import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-pass-dialog',
  templateUrl: './passDialog.component.html',
  styleUrls: ['./passDialog.component.css'],
})
export class PassDialogComponent {
  resetPassword: boolean = false;
  passwords = {
    oldPassword: '',
    newPassword: '',
    confirmPassword: '',
  };

  constructor(
    public dialogRef: MatDialogRef<PassDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { id: number; userName: string }
  ) {}

  onResetToggleChange(): void {
    this.passwords = {
      oldPassword: '',
      newPassword: '',
      confirmPassword: '',
    };
  }

  passwordsMatch(): boolean {
    // Return true if passwords match, false if they don't match
    return this.passwords.newPassword === this.passwords.confirmPassword;
  }

  showPasswordMismatchError(): boolean {
    return (
      !this.passwordsMatch() &&
      this.passwords.newPassword.length > 0 &&
      this.passwords.confirmPassword.length > 0
    );
  }

  isFormValid(): boolean {
    if (this.resetPassword) {
      return true;
    }

    return (
      this.passwords.oldPassword.length > 0 &&
      this.passwords.newPassword.length >= 8 &&
      this.passwords.confirmPassword.length > 0 &&
      this.passwordsMatch()
    );
  }

  onSubmit(): void {
    const result = {
      userId: this.data.id,
      reset: this.resetPassword,
      passwords: this.resetPassword ? null : this.passwords,
    };

    this.dialogRef.close(result);
  }
}
