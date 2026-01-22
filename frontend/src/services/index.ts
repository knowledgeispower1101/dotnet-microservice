import { categoryApi, type Category } from './category';
export { categoryApi };
export type { Category };
export interface ApiResponse<T> {
  data: T;
  message?: string;
  status?: number;
}
