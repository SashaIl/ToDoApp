import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { Category, CreateCategoryDto } from '../../models/category.model';
import { ApiResponse } from '../../models/api-response.model';

@Injectable({ providedIn: 'root' })
export class CategoryService {
  private apiUrl = 'https://localhost:7173/api/task';

  constructor(private http: HttpClient) {}

  getCategories(): Observable<ApiResponse<Category[]>> {
    return this.http.get<ApiResponse<Category[]>>(`${this.apiUrl}/get_user_categories`)
    .pipe(
        tap(response => console.log('Fetched categories:', response))
    );
  }

  createCategory(dto: CreateCategoryDto): Observable<ApiResponse<Category>> {
    console.log(dto);
    
    return this.http.post<ApiResponse<Category>>(`${this.apiUrl}/add_category`, dto);
  }
}