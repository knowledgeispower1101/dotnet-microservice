import { useLocation } from 'react-router-dom';

export type HeaderVariant = 'cart' | 'shop' | 'default' | 'login' | 'register';

export const useGetPath = (): HeaderVariant => {
  const location = useLocation();
  const pathname = location.pathname;

  if (pathname.includes('/auth/login')) return 'login';
  
  if (pathname.includes('/auth/register')) return 'register';
  
  if (pathname.includes('/cart'))  return 'cart';
  
  if (pathname.includes('/shop')) return 'shop';

  return 'default';
};
