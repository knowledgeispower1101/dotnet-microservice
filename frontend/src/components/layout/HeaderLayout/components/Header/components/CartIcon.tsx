import { ShoppingCart } from 'lucide-react';
import { useEffect, useRef, useState } from 'react';
import { Link } from 'react-router-dom';

const CartIcon = () => {
  const [openCart, setOpenCart] = useState(false);
  const cartRef = useRef<HTMLDivElement>(null);
  useEffect(() => {
    const handleClickOutside = (e: MouseEvent) => {
      if (cartRef.current && !cartRef.current.contains(e.target as Node)) {
        setOpenCart(false);
      }
    };
    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, []);
  return (
    <div ref={cartRef} className="relative">
      <button onClick={() => setOpenCart((v) => !v)} className="relative text-white text-2xl">
        <ShoppingCart />
        <span className="absolute -top-2 -right-2 min-w-5 h-5 px-1 bg-white text-[#F6412D] text-xs rounded-full flex items-center justify-center">
          3
        </span>
      </button>

      {openCart && (
        <div className="absolute right-0 top-10 w-80 bg-white shadow-xl border border-gray-200 z-50">
          <div className="p-4 text-sm text-gray-700">
            <div className="font-medium mb-2">Sản phẩm trong giỏ</div>
            <div className="space-y-2">
              <div className="flex justify-between">
                <span>iPhone 15 Pro Max</span>
                <span>₫30.000.000</span>
              </div>
              <div className="flex justify-between">
                <span>AirPods Pro</span>
                <span>₫5.000.000</span>
              </div>
            </div>
            <Link to="/cart" className="mt-4 block bg-[#fb5533] text-white rounded-sm text-center p-2">
              Xem giỏ hàng
            </Link>
          </div>
        </div>
      )}
    </div>
  );
};
export default CartIcon;
