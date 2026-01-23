import { useState, useEffect, memo } from 'react';
import flashSaleImg from '@/assets/flash-sale.png';
import { Link } from 'react-router-dom';
import { MyCarousel } from '@/components';
import { FLASH_SALE_PRODUCTS } from '@/constants';
import type { FlashSaleProduct } from '@/constants';

const FlashSaleItem = memo(({ product }: { product: FlashSaleProduct }) => {
  const progressPercent = (product.soldCount / product.totalStock) * 100;

  return (
    <div className="bg-white rounded-lg overflow-hidden hover:shadow-lg transition-shadow cursor-pointer border border-gray-100">
      {/* Product Image */}
      <div className="relative bg-gray-50 h-44 flex items-center justify-center">
        <span className="text-7xl">{product.image}</span>
        <div className="absolute top-0 right-0 bg-[#FFCE3D] text-[#EE4D2D] font-bold px-2 py-1 text-xs">-{product.discountPercent}%</div>
      </div>

      {/* Product Info */}
      <div className="p-3">
        {/* Price */}
        <div className="flex items-center justify-center mb-2">
          <span className="text-[#EE4D2D] text-xl font-bold">₫{product.finalPrice.toLocaleString('vi-VN')}</span>
        </div>

        {/* Progress Bar */}
        <div className="relative h-4 bg-[#FFE5DD] rounded-full overflow-hidden">
          <div
            className="absolute top-0 left-0 h-full bg-linear-to-r from-[#EE4D2D] to-[#F05D40] rounded-full transition-all"
            style={{ width: `${progressPercent}%` }}
          />
          <div className="absolute inset-0 flex items-center justify-center text-xs font-medium text-white">ĐÃ BÁN {product.soldCount}</div>
        </div>
      </div>
    </div>
  );
});

// Countdown Timer Component
const CountdownTimer = () => {
  const [timeLeft, setTimeLeft] = useState({ hours: 2, minutes: 6, seconds: 11 });

  useEffect(() => {
    const timer = setInterval(() => {
      setTimeLeft((prev) => {
        let { hours, minutes, seconds } = prev;

        if (seconds > 0) {
          seconds--;
        } else if (minutes > 0) {
          minutes--;
          seconds = 59;
        } else if (hours > 0) {
          hours--;
          minutes = 59;
          seconds = 59;
        } else {
          return { hours: 2, minutes: 0, seconds: 0 };
        }

        return { hours, minutes, seconds };
      });
    }, 1000);

    return () => clearInterval(timer);
  }, []);

  const formatTime = (num: number) => num.toString().padStart(2, '0');

  return (
    <div className="flex items-center gap-1 ml-3">
      <div className="bg-black text-white px-1.5 py-0.5 rounded text-sm font-bold min-w-6 text-center">{formatTime(timeLeft.hours)}</div>
      <div className="bg-black text-white px-1.5 py-0.5 rounded text-sm font-bold min-w-6 text-center">{formatTime(timeLeft.minutes)}</div>
      <div className="bg-black text-white px-1.5 py-0.5 rounded text-sm font-bold min-w-6 text-center">{formatTime(timeLeft.seconds)}</div>
    </div>
  );
};

const FlashSaleCarousel = () => {
  return (
    <div className="bg-white mt-5">
      <div className="flex flex-row justify-between items-center bg-white h-7.5 px-5 py-3.75">
        <div className="flex flex-row items-center">
          <div
            className="uppercase w-32.5 h-7.5 leading-7.5 bg-center bg-no-repeat bg-contain"
            style={{ backgroundImage: `url(${flashSaleImg})` }}
          ></div>
          <CountdownTimer />
        </div>
        <div>
          <Link className="text-[#F6412D] font-bold" to="/flash-sale">
            Xem tất cả
          </Link>
        </div>
      </div>
      <div className="px-5 py-4">
        <MyCarousel items={FLASH_SALE_PRODUCTS} itemsPerRow={6} rows={1} renderItem={(item) => <FlashSaleItem product={item} />} />
      </div>
    </div>
  );
};

export default FlashSaleCarousel;
