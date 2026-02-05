import { api } from '@/lib';
import type { User } from '@/store';

export interface RegisterPayload {
  email: string;
  password: string;
  lastName: string;
  firstName: string;
}

export interface LoginPayload {
  email: string;
  password: string;
}

export interface LoginResponse {
  userId: number;
  email: string;
  refreshToken: string;
  lastName: string;
  firstName: string;
  accessToken: string;
}

export interface MeResponse {
  accessToken: string;
  user: User;
}

export const userApi = {
  register: async (payload: RegisterPayload) => {
    const res = await api.post('/auth/register', payload);
    return res.data;
  },
  login: async (payload: LoginPayload) => {
    const res = await api.post('/auth/login', payload);
    return res.data;
  },
  getCurrentUser: async () => {
    const res = await api.get('/auth/profile', {
      withCredentials: true,
    });
    return res.data;
  },
  logout: async () => {
    const res = await api.post('/auth/logout', {
      withCredentials: true,
    });
    return res.data;
  },
};
