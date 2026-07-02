import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task, CreateTaskDto, UpdateTaskDto } from '../../models/task.model';
import { ApiResponse } from '../../models/api-response.model';

@Injectable({ providedIn: 'root' })
export class TaskService {
    private apiUrl = 'https://localhost:7173/api/task';

    constructor(private http: HttpClient) { }

    getTasks(page: number = 1, pageSize: number = 10, search?: string, category?: string): Observable<ApiResponse<Task[]>> {
        let params = new HttpParams()
            .set('page', page)
            .set('pageSize', pageSize);

        if (search) params = params.set('search', search);
        if (category) params = params.set('category', category);

        return this.http.get<ApiResponse<Task[]>>(`${this.apiUrl}/get_user_tasks`, { params });
    }

    createTask(dto: CreateTaskDto): Observable<Task> {
        return this.http.post<Task>(`${this.apiUrl}/add_task`, dto);
    }

    updateTask(id: string, dto: UpdateTaskDto): Observable<Task> {
        return this.http.put<Task>(`${this.apiUrl}/update_task/${id}`, dto);
    }

    deleteTask(id: string): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/delete_task/${id}`);
    }

    getTotalCountOfTask(search?: string, category?: string): Observable<ApiResponse<number>> {
        return this.http.get<ApiResponse<number>>(`${this.apiUrl}/get_total_count_of_task`, {
            params: new HttpParams()
                .set('search', search || '')
                .set('category', category || '')
        });
    }
}