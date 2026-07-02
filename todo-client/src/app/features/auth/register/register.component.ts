import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { CommonModule } from '@angular/common';


@Component({
    selector: 'app-register',
    standalone: true,
    imports: [FormsModule, RouterLink, CommonModule],
    templateUrl: './register.component.html'
})
export class RegisterComponent {
    email = '';
    password = '';
    error = '';
    showPassword = false;

    constructor(private authService: AuthService, private router: Router) {}

    onSubmit() {
        console.log(this.password);
        
    this.authService.register({ email: this.email, pass: this.password })
        .subscribe({
        next: () => this.router.navigate(['/login']),
        error: (resp) => this.error = resp.message || 'Registration failed'
        });
    }   
}