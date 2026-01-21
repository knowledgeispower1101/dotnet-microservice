import { lazy } from 'react';
import { cn } from '@/lib/utils';
import type { HeaderVariant } from '../shared';
const Logo = lazy(() => import('./Logo'));
const SearchSection = lazy(() => import('../components/SearchSection'));
const CartIcon = lazy(() => import('../components/CartIcon'));

const VARIANT_CONFIG = {
  login: { title: 'Đăng Nhập', showHelp: true },
  register: { title: 'Đăng Ký', showHelp: true },
  cart: { title: 'Giỏ hàng', showHelp: false },
} as const;

type Props = {
  variant: HeaderVariant;
};

const HeaderContent = ({ variant }: Props) => {
  const isAuthPage = variant === 'login' || variant === 'register';
  const isCartPage = variant === 'cart';
  const config = VARIANT_CONFIG[variant as keyof typeof VARIANT_CONFIG];
  return (
    <div className="max-w-300 mx-auto h-21.25 px-4 flex items-center justify-between pt-4 pb-2.5">
      <div className="pr-10 flex justify-between items-end">
        <Logo variant={variant} />

        {config && (
          <div
            className={cn(
              'ml-3.75 mb-px h-7.5 border-l border-[#ee4d2d] pl-3.75 text-xl leading-7.5 capitalize',
              isAuthPage && 'text-inherit',
              isCartPage && 'text-[#ee4d2d]',
            )}
          >
            {config.title}
          </div>
        )}
      </div>

      {config?.showHelp && <div className="text-[#ee4d2d] cursor-pointer text-sm mr-3.75 hover:opacity-80 transition-opacity">Bạn cần giúp đỡ?</div>}

      {!isAuthPage && !isCartPage && (
        <div className="flex items-center gap-4">
          <SearchSection />
        </div>
      )}
      {!isAuthPage && !isCartPage && (
        <div className="flex items-center gap-4">
          <CartIcon />
        </div>
      )}
    </div>
  );
};

export default HeaderContent;
