import { categoryApi, type Category } from './category';
import { userApi } from './user';
export { categoryApi, userApi };
export type { Category };

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
