import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

export interface UserDto {
  id: number;
  userName: string;
  empName: string;
  natId: string;
  deptId: number;
  deptName: string;
  extClctr: boolean;
  stopped: boolean;
  workGroupIds: number[];
  workGroups: string[];
}
export interface UserSaveDto {
  userName: string;
  empName: string;
  natId: string;
  deptId: number | null;
  extClctr: boolean;
  stopped: boolean;
  workGroupIds: number[];
}
export interface PaginatedResult<T> {
  items: T[];
  totalCount: number;
}

export enum OrderBy {
  Id = 0,
  Name = 1,
  UserName = 2,
}

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseUrl = 'https://localhost:7285/api/';

  constructor(private http: HttpClient) {}

  getUsers(
    pageNumber: number = 1,
    pageSize: number = 10,
    ascending: boolean = false,
    orderBy: OrderBy = OrderBy.Id
  ): Observable<PaginatedResult<UserDto>> {
    console.log(orderBy, ascending);
    let params = new HttpParams();
    const addParam = (key: string, value: any) => {
      if (value !== undefined && value !== null) {
        params = params.set(key, value.toString());
      }
    };
    addParam('pageSize', pageSize);
    addParam('pageNumber', pageNumber);
    addParam('ascending', ascending);
    addParam('orderBy', orderBy);
    return this.http.get<PaginatedResult<UserDto>>(`${this.baseUrl}User/get`, {
      params,
    });
  }
  searchUsers(searchQuery: string): Observable<PaginatedResult<UserDto>> {
    return this.http.get<PaginatedResult<UserDto>>(
      `${this.baseUrl}User/search?searchQuery=${searchQuery}`
    );
  }
  getDepts(): Observable<any[]> {
    return this.http
      .get<{ items: any[]; totalCount: number }>(`${this.baseUrl}dept/get`)
      .pipe(
        map((response: { items: any[]; totalCount: number }) => response.items)
      );
  }
  getGroups(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}group`);
  }
  addUser(data: UserSaveDto): Observable<UserSaveDto> {
    return this.http.post<UserSaveDto>(`${this.baseUrl}User/add`, data);
  }
  editUser(id: number, data: UserSaveDto): Observable<UserSaveDto> {
    console.log('Editing user with ID:', id, 'Data:', data);
    return this.http.put<UserSaveDto>(`${this.baseUrl}User/update/${id}`, data);
  }
  editPassword(id: number, passwords: any, reset: boolean): Observable<any> {
    let url = `${this.baseUrl}User/updatePassword/${id}?reset=${reset}`;

    // Only add password parameters if we're not resetting and passwords exist
    if (!reset && passwords) {
      url += `&oldPassword=${encodeURIComponent(
        passwords.oldPassword
      )}&newPassword=${encodeURIComponent(passwords.newPassword)}`;
    }

    return this.http.put(url, null, { responseType: 'text' });
  }
}
