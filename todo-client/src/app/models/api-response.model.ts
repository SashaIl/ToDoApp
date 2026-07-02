export interface ApiResponse<T> {
  message: string | null;
  result: T;
}