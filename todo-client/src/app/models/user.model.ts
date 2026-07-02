export interface RegisterDto {
  email: string;
  pass: string;
}

export interface LoginDto {
  email: string;
  password: string;
}

export interface AuthResponse {
  message: string | null;
  result: string;  // JWT token
}