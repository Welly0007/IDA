import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

export interface DeptDto {
  id: number;
  name: string;
}

interface PaginatedDepts {
  items: DeptDto[];
  totalCount: number;
}

@Injectable({
  providedIn: 'root',
})
export class DeptService {
  private baseUrl = 'https://localhost:7285/api/dept';

  constructor(private http: HttpClient) {}

  getDepts(
    pageNumber?: number,
    pageSize?: number,
    ascending?: boolean
  ): Observable<PaginatedDepts> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber?.toString() || '')
      .set('pageSize', pageSize?.toString() || '')
      .set('ascending', ascending ? 'true' : 'false');

    return this.http
      .get<PaginatedDepts>(`${this.baseUrl}/get`, {
        params,
      })
      .pipe(map((response) => response));
  }
  updateDept(dept: DeptDto): Observable<DeptDto> {
    return this.http.put<DeptDto>(`${this.baseUrl}/${dept.id}`, dept);
  }
  addDept(dept: { name: string }): Observable<DeptDto> {
    return this.http.post<DeptDto>(`${this.baseUrl}`, dept);
  }
  deleteDept(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
  searchDepts(term: string): Observable<DeptDto[]> {
    const params = new HttpParams().set('searchQuery', term);
    return this.http.get<DeptDto[]>(`${this.baseUrl}/search`, { params });
  }
}
