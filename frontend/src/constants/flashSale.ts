/**
 * Flash Sale Product Interface and Mock Data
 */

export interface FlashSaleProduct {
  id: number;
  name: string;
  image: string;
  originalPrice: number;
  discountPercent: number;
  finalPrice: number;
  soldCount: number;
  totalStock: number;
}

export const FLASH_SALE_PRODUCTS: FlashSaleProduct[] = [
  {
    id: 1,
    name: 'Áo Thun Nam Basic',
    image: '👕',
    originalPrice: 150000,
    discountPercent: 50,
    finalPrice: 75000,
    soldCount: 120,
    totalStock: 200,
  },
  {
    id: 2,
    name: 'Giày Sneaker Thể Thao',
    image: '👟',
    originalPrice: 500000,
    discountPercent: 60,
    finalPrice: 200000,
    soldCount: 85,
    totalStock: 100,
  },
  {
    id: 3,
    name: 'Balo Laptop',
    image: '🎒',
    originalPrice: 350000,
    discountPercent: 45,
    finalPrice: 192500,
    soldCount: 45,
    totalStock: 150,
  },
  {
    id: 4,
    name: 'Tai Nghe Bluetooth',
    image: '🎧',
    originalPrice: 250000,
    discountPercent: 70,
    finalPrice: 75000,
    soldCount: 180,
    totalStock: 200,
  },
  {
    id: 5,
    name: 'Đồng Hồ Thông Minh',
    image: '⌚',
    originalPrice: 800000,
    discountPercent: 55,
    finalPrice: 360000,
    soldCount: 30,
    totalStock: 80,
  },
  {
    id: 6,
    name: 'Túi Xách Nữ',
    image: '👜',
    originalPrice: 280000,
    discountPercent: 40,
    finalPrice: 168000,
    soldCount: 95,
    totalStock: 120,
  },
  {
    id: 7,
    name: 'Kính Mát Thời Trang',
    image: '🕶️',
    originalPrice: 180000,
    discountPercent: 65,
    finalPrice: 63000,
    soldCount: 150,
    totalStock: 180,
  },
  {
    id: 8,
    name: 'Dép Sandal',
    image: '🩴',
    originalPrice: 120000,
    discountPercent: 50,
    finalPrice: 60000,
    soldCount: 200,
    totalStock: 250,
  },
  {
    id: 9,
    name: 'Mũ Lưỡi Trai',
    image: '🧢',
    originalPrice: 90000,
    discountPercent: 35,
    finalPrice: 58500,
    soldCount: 70,
    totalStock: 150,
  },
  {
    id: 10,
    name: 'Ví Cầm Tay',
    image: '💼',
    originalPrice: 220000,
    discountPercent: 48,
    finalPrice: 114400,
    soldCount: 55,
    totalStock: 100,
  },
];
