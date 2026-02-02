import { useEffect } from 'react';
import { useAuthStore } from '@/store';
import { authHooks } from '@/hooks';

const useAuthInit = () => {
  const setAuth = useAuthStore((s) => s.setAuth);
  const clearAuth = useAuthStore((s) => s.clearAuth);
  const { useCurrentUser } = authHooks;
  const { data, isSuccess, isError } = useCurrentUser();

  useEffect(() => {
    if (isSuccess && data?.data) {
      const { accessToken, user } = data.data;
      setAuth(accessToken, user);
    }

    if (isError) {
      clearAuth();
    }
  }, [isSuccess, isError, data, setAuth, clearAuth]);
};

export default useAuthInit;
