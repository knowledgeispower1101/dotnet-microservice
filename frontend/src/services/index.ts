import { userApi } from './user';
import type { LoginResponse, MeResponse, RegisterPayload, LoginPayload } from './user';
import type { ImageItem, ImageUploadProps } from './media';

export { userApi };
export type {
  LoginResponse,
  MeResponse,
  RegisterPayload,
  LoginPayload,
  ImageItem,
  ImageUploadProps,
};

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
