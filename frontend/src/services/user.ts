import { api } from '@/lib';
import type { ApiResponse } from '.';

export interface RegisterPayload {
  email: string;
  password: string;
  lastName: string;
  firstName: string;
}

export const userApi = {
  register: async (payload: RegisterPayload): Promise<ApiResponse<string>> => {
    const res = await api.post('/auth/register', payload);
    return res.data;
  },
};
