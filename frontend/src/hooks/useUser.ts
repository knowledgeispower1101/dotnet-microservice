import { useMutation, useQuery } from '@tanstack/react-query';
import { userApi } from '@/services';
import type {
  LoginPayload,
  LoginResponse,
  MeResponse,
  RegisterPayload,
  ApiError,
  ApiResponse,
} from '@/services';
import { AxiosError } from 'axios';

const authHooks = {
  useRegister: () =>
    useMutation<ApiResponse<string>, AxiosError<ApiError>, RegisterPayload>({
      mutationFn: userApi.register,
    }),

  useLogin: () =>
    useMutation<ApiResponse<LoginResponse>, AxiosError<ApiError>, LoginPayload>({
      mutationFn: userApi.login,
    }),

  useCurrentUser: () =>
    useQuery<ApiResponse<MeResponse>>({
      queryKey: ['auth', 'me'],
      queryFn: userApi.getCurrentUser,
      retry: false,
    }),

  useLogout: () =>
    useMutation({
      mutationFn: userApi.logout,
    }),
};

export default authHooks;
