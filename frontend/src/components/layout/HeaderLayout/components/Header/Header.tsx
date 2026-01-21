import { Suspense, lazy } from 'react';
import { cn } from '@/lib/utils';
import { useGetPath, type HeaderVariant } from './shared';

const HeaderContent = lazy(() => import('./components/HeaderContent'));

const HEADER_BG: Record<HeaderVariant, string> = {
  default: 'bg-[#F6412D]',
  cart: 'bg-white',
  shop: 'bg-[#D0011C]',
  register: 'bg-white',
  login: 'bg-white',
};

export const Header = () => {
  const variant = useGetPath();
  const isAuthPage = variant === 'login' || variant === 'register';
  const isCartPage = variant === 'cart';
  return (
    <header
      className={cn(
        'w-full sticky top-8 z-40 transition-colors duration-200',
        HEADER_BG[variant],
        isCartPage && 'border-b border-[#00000017]',
        isAuthPage ? 'static top-0' : 'sticky',
      )}
    >
      <Suspense fallback={<div className="max-w-300 mx-auto h-21.25 px-4" />}>
        <HeaderContent variant={variant} />
      </Suspense>
    </header>
  );
};
