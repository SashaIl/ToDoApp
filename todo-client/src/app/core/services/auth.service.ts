import { HttpClient } from "@angular/common/http";
import { AuthResponse, LoginDto, RegisterDto } from "../../models/user.model";
import { Observable, tap } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class AuthService{
    private baseUrl = 'https://localhost:7173/api/user';
    private jwtTokenNameForLs = "token";

    constructor(private http: HttpClient){}

    register(dto: RegisterDto): Observable<AuthResponse> {
        return this.http.post<AuthResponse>(`${this.baseUrl}/add_user`,dto);
    }

    login(dto: LoginDto): Observable<AuthResponse> {
        return this.http.post<AuthResponse>(`${this.baseUrl}/login/`, dto)
        .pipe(
            tap(response => this.saveToken(response.result))
        );
    }

    logout(): void {
        localStorage.removeItem(this.jwtTokenNameForLs);
    }

    getToken(): string | null{
        return localStorage.getItem(this.jwtTokenNameForLs);
    }

    isLoggedIn(): boolean {
        return this.getToken() !== null;
    }

    private saveToken(token: string): void{
        localStorage.setItem(this.jwtTokenNameForLs, token);
    }
}