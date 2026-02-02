import { categoryApi, type Category } from './category';
import { userApi } from './user';
import type { LoginResponse, MeResponse, RegisterPayload, LoginPayload } from './user';
export { categoryApi, userApi };
export type { Category, LoginResponse, MeResponse, RegisterPayload, LoginPayload };

export interface ApiResponse<T> {
  success: boolean;
  data: T;
  message?: string;
  errorCode?: string;
}

export interface ApiError {
  message: string;
  errorCode: string;
}
