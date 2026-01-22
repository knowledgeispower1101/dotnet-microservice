import { useCallback, useState } from 'react';
import type { AxiosRequestConfig } from 'axios';
import { api } from '@/lib';
import type { ApiResponse } from '@/services';

export function useFetch<T>() {
  const [data, setData] = useState<T | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const execute = useCallback(async (config: AxiosRequestConfig): Promise<T> => {
    try {
      setLoading(true);
      setError(null);

      const res = await api.request<ApiResponse<T> | T>(config);

      const result = typeof res.data === 'object' && res.data !== null && 'data' in res.data ? res.data.data : res.data;

      setData(result);
      return result;
    } catch (err: unknown) {
      if (err instanceof Error) {
        setError(err.message);
      } else {
        setError('Unknown error');
      }
      throw err;
    } finally {
      setLoading(false);
    }
  }, []);

  return {
    data,
    loading,
    error,
    execute,
  };
}
