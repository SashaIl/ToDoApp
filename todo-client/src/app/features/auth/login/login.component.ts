import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [FormsModule, RouterLink,CommonModule],
    templateUrl: './login.component.html'
})
export class LoginComponent {
    email = '';
    password = '';
    error = '';

    constructor(private authService: AuthService, private router: Router) {}

    onSubmit() {
    this.authService.login({ email: this.email, password: this.password })
        .subscribe({
        next: () => this.router.navigate(['/tasks']),
        error: (resp) => this.error = 'Invalid email or password'
        });
    }
}