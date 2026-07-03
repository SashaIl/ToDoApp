import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { TaskService } from '../../../core/services/task.service';
import { AuthService } from '../../../core/services/auth.service';
import { Task, CreateTaskDto, UpdateTaskDto } from '../../../models/task.model';
import { CategoryService } from '../../../core/services/category.service';

import { Category } from '../../../models/category.model';
@Component({
    selector: 'app-task-list',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './task-list.component.html'
})
export class TaskListComponent implements OnInit {
    tasks: Task[] = [];
    categories: Category[] = [];  
    totalCount = 0;
    page = 1;
    pageSize = 10;
    search = '';
    selectedCategory = '';
    error = '';

    
    totalPages = 1;    

    showForm = false;
    editingTask: Task | null = null;
    formName = '';
    formDescription = '';
    formCategory = '';

    //new category
    showCategoryForm = false;
    newCategoryName = '';

    constructor(
        private taskService: TaskService,
        private categoryService: CategoryService,
        private authService: AuthService,
        private router: Router
    ) { }

    ngOnInit() {
        this.loadCategories();
        this.reloadTasksAndPagination();
    }

    loadTotalPages(){
        this.taskService.getTotalCountOfTask(this.search || "", this.selectedCategory).subscribe({
            next: (count) => {
                console.log(count);
                
                this.totalPages = Math.ceil(count.result / this.pageSize);
            }
        })
    }

    loadCategories() {
        this.categoryService.getCategories().subscribe({
            next: (data) => this.categories = data.result,
            error: () => this.error = 'Failed to load categories'
        });
    }


    reloadTasksAndPagination() {
        this.loadTasks();
        this.loadTotalPages();
    }

    createCategory() {
        if (!this.newCategoryName.trim()) return;

        this.categoryService.createCategory({ name: this.newCategoryName }).subscribe({
            next: () => {
                this.newCategoryName = '';
                this.showCategoryForm = false;
                this.loadCategories();  
            },
            error: () => this.error = 'Failed to create category'
        });
    }

    loadTasks() {
        console.log(this.selectedCategory);
        
        this.taskService.getTasks(
            this.page,
            this.pageSize,
            this.search || undefined,
            this.selectedCategory || undefined
        ).subscribe({
            next: (data) => {
                this.tasks = data.result;  
            },
            error: () => this.error = 'Failed to load tasks'
        });
    }

    goToPage(p: number) {
        if (p < 1 || p > this.totalPages) return;
        this.page = p;
        this.loadTasks();
    }

    onSearch() {
        this.page = 1; 
        this.reloadTasksAndPagination();
    }

    onCategoryFilter() {
        this.page = 1;
        this.reloadTasksAndPagination();
    }

    // creating form
    openCreateForm() {
        this.editingTask = null;
        this.formName = '';
        this.formDescription = '';
        this.formCategory = 'Other';
        this.showForm = true;
    }

    // edit form
    openEditForm(task: Task) {
        this.editingTask = task;
        this.formName = task.name;
        this.formDescription = task.description;
        this.formCategory = task.category;
        this.showForm = true;
    }

    closeForm() {
        this.showForm = false;
    }

    onSubmitForm() {
        if (this.editingTask) {
            const dto: UpdateTaskDto = {
                name: this.formName,
                description: this.formDescription,
                category: this.formCategory
            };
            this.taskService.updateTask(this.editingTask.id, dto).subscribe({
                next: () => {
                    this.closeForm();
                    this.loadTasks();
                },
                error: () => this.error = 'Failed to update task'
            });
        } else {
            const dto: CreateTaskDto = {
                name: this.formName,
                description: this.formDescription,
                category: this.formCategory
            };
            this.taskService.createTask(dto).subscribe({
                next: () => {
                    this.closeForm();
                    this.loadTasks();
                },
                error: () => this.error = 'Failed to create task'
            });
        }
    }

    deleteTask(id: string) {
        if (!confirm('Delete this task?')) return;
        this.taskService.deleteTask(id).subscribe({
            next: () => this.reloadTasksAndPagination(),
            error: () => this.error = 'Failed to delete task'
        });
    }

    logout() {
        this.authService.logout();
        this.router.navigate(['/login']);
    }
}