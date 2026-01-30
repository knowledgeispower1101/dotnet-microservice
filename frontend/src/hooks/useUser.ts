import { useMutation } from '@tanstack/react-query';
import { userApi, type ApiError, type ApiResponse } from '@/services';
import type { RegisterPayload } from '@/services/user';
import type { AxiosError } from 'axios';

export const useRegister = () => {
  return useMutation<ApiResponse<string>, AxiosError<ApiError>, RegisterPayload>({
    mutationFn: userApi.register,
  });
};
