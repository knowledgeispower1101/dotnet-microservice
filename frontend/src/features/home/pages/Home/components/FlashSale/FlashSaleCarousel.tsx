import { useState, useEffect } from 'react';
import flashSaleImg from '@/assets/flash-sale.png';
import { Link } from 'react-router-dom';
import { MyCarousel } from '@/components';

interface FlashSaleProduct {
  id: number;
  name: string;
  image: string;
  originalPrice: number;
  discountPercent: number;
  finalPrice: number;
  soldCount: number;
  totalStock: number;
}

const flashSaleProducts: FlashSaleProduct[] = [
  { id: 1, name: 'Áo Thun Nam Basic', image: '👕', originalPrice: 150000, discountPercent: 50, finalPrice: 75000, soldCount: 120, totalStock: 200 },
  { id: 2, name: 'Giày Sneaker Thể Thao', image: '👟', originalPrice: 500000, discountPercent: 60, finalPrice: 200000, soldCount: 85, totalStock: 100 },
  { id: 3, name: 'Balo Laptop', image: '🎒', originalPrice: 350000, discountPercent: 45, finalPrice: 192500, soldCount: 45, totalStock: 150 },
  { id: 4, name: 'Tai Nghe Bluetooth', image: '🎧', originalPrice: 250000, discountPercent: 70, finalPrice: 75000, soldCount: 180, totalStock: 200 },
  { id: 5, name: 'Đồng Hồ Thông Minh', image: '⌚', originalPrice: 800000, discountPercent: 55, finalPrice: 360000, soldCount: 30, totalStock: 80 },
  { id: 6, name: 'Túi Xách Nữ', image: '👜', originalPrice: 280000, discountPercent: 40, finalPrice: 168000, soldCount: 95, totalStock: 120 },
  { id: 7, name: 'Kính Mát Thời Trang', image: '🕶️', originalPrice: 180000, discountPercent: 65, finalPrice: 63000, soldCount: 150, totalStock: 180 },
  { id: 8, name: 'Dép Sandal', image: '🩴', originalPrice: 120000, discountPercent: 50, finalPrice: 60000, soldCount: 200, totalStock: 250 },
  { id: 9, name: 'Mũ Lưỡi Trai', image: '🧢', originalPrice: 90000, discountPercent: 35, finalPrice: 58500, soldCount: 70, totalStock: 150 },
  { id: 10, name: 'Ví Cầm Tay', image: '💼', originalPrice: 220000, discountPercent: 48, finalPrice: 114400, soldCount: 55, totalStock: 100 },
];

const FlashSaleItem = ({ product }: { product: FlashSaleProduct }) => {
  const progressPercent = (product.soldCount / product.totalStock) * 100;

  return (
    <div className="bg-white rounded-lg overflow-hidden hover:shadow-lg transition-shadow cursor-pointer border border-gray-100">
      {/* Product Image */}
      <div className="relative bg-gray-50 h-44 flex items-center justify-center">
        <span className="text-7xl">{product.image}</span>
        <div className="absolute top-0 right-0 bg-[#FFCE3D] text-[#EE4D2D] font-bold px-2 py-1 text-xs">
          -{product.discountPercent}%
        </div>
      </div>

      {/* Product Info */}
      <div className="p-3">
        {/* Price */}
        <div className="flex items-center justify-center mb-2">
          <span className="text-[#EE4D2D] text-xl font-bold">
            ₫{product.finalPrice.toLocaleString('vi-VN')}
          </span>
        </div>

        {/* Progress Bar */}
        <div className="relative h-4 bg-[#FFE5DD] rounded-full overflow-hidden">
          <div
            className="absolute top-0 left-0 h-full bg-gradient-to-r from-[#EE4D2D] to-[#F05D40] rounded-full transition-all"
            style={{ width: `${progressPercent}%` }}
          />
          <div className="absolute inset-0 flex items-center justify-center text-xs font-medium text-white">
            ĐÃ BÁN {product.soldCount}
          </div>
        </div>
      </div>
    </div>
  );
};

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
      <div className="bg-black text-white px-1.5 py-0.5 rounded text-sm font-bold min-w-[24px] text-center">
        {formatTime(timeLeft.hours)}
      </div>
      <div className="bg-black text-white px-1.5 py-0.5 rounded text-sm font-bold min-w-[24px] text-center">
        {formatTime(timeLeft.minutes)}
      </div>
      <div className="bg-black text-white px-1.5 py-0.5 rounded text-sm font-bold min-w-[24px] text-center">
        {formatTime(timeLeft.seconds)}
      </div>
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
      <div className="px-5 pb-4">
        <MyCarousel items={flashSaleProducts} itemsPerRow={6} rows={1} renderItem={(item) => <FlashSaleItem product={item} />} />
      </div>
    </div>
  );
};

export default FlashSaleCarousel;
