import React from 'react';
import { Zap, Package, Flame, Shirt, Award, Tag } from 'lucide-react';
import { Container } from '@/components';

interface NavigationItem {
  icon: React.ReactNode;
  label: string;
}

export const BannerSection = () => {
  const navigationItems: NavigationItem[] = [
    { icon: <Zap className="w-6 h-6 text-orange-500" />, label: 'Deal Từ 1.000Đ' },
    { icon: <Package className="w-6 h-6 text-red-500" />, label: 'Shopee Xu Lý' },
    { icon: <Flame className="w-6 h-6 text-orange-500" />, label: 'Deal Hot Giờ Vàng' },
    { icon: <Shirt className="w-6 h-6 text-orange-500" />, label: 'Shopee Style Giảm 30%' },
    { icon: <Award className="w-6 h-6 text-orange-500" />, label: 'Khách Hàng Thân Thiết' },
    { icon: <Tag className="w-6 h-6 text-orange-500" />, label: 'Mã Giảm Giá' },
  ];

  return (
    <div className="w-full bg-white pt-7.5 shadow-[0_1px_1px_#0000000d]">
      {/* Main Banner Section */}
      <Container>
        <div className="grid grid-cols-1 md:grid-cols-3 gap-2 bg-gray-50">
          {/* Large Banner - Left (2 columns) */}
          <div className="md:col-span-2 relative bg-linear-to-r from-orange-500 via-red-500 to-orange-400 rounded-lg overflow-hidden shadow-lg">
            <div className="absolute top-4 left-4 bg-white px-3 py-1 rounded-md font-bold text-orange-500">15.1</div>

            <div className="flex items-center justify-between p-8 h-full">
              <div className="flex items-center space-x-4">
                <div className="bg-yellow-400 rounded-full w-16 h-16 flex items-center justify-center animate-pulse">
                  <span className="text-2xl">🎁</span>
                </div>
                <div className="text-white">
                  <div className="flex items-center space-x-2">
                    <span className="bg-blue-600 px-3 py-1 rounded text-sm font-bold">SHOPEE</span>
                    <span className="text-4xl font-black">SIÊU RẺ</span>
                  </div>
                  <div className="mt-4 bg-white rounded-lg p-3 text-black">
                    <div className="flex items-center space-x-4">
                      <div className="text-center">
                        <div className="text-sm">ĐỒNG GIÁ</div>
                        <div className="text-orange-500 font-bold text-xl">1.000Đ</div>
                        <div className="text-xs">TỪ</div>
                      </div>
                      <div className="text-center">
                        <div className="text-sm">VOUCHER LIVE</div>
                        <div className="text-orange-500 font-bold text-xl">88%</div>
                        <div className="text-xs">ĐẾN</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <div className="relative">
                <div className="absolute -top-8 -right-8 bg-blue-600 text-white px-4 py-2 rounded-lg transform rotate-12">
                  <div className="text-center">
                    <div className="text-2xl font-bold">FREESHIP</div>
                    <div className="text-5xl font-black text-yellow-300">0Đ</div>
                  </div>
                </div>
                <div className="bg-yellow-300 rounded-full w-24 h-24 flex items-center justify-center mt-8">
                  <span className="text-4xl">⚡</span>
                </div>
              </div>
            </div>
          </div>

          {/* Right Column - 2 Small Banners */}
          <div className="flex flex-col gap-2">
            {/* Top Small Banner */}
            <div className="bg-linear-to-r from-teal-400 to-emerald-400 rounded-lg p-4 shadow-lg flex-1">
              <div className="absolute top-2 left-2 bg-white px-2 py-1 rounded text-xs font-bold text-teal-600">15.1</div>
              <div className="flex items-center justify-between h-full">
                <div className="text-white">
                  <div className="text-sm font-semibold">CÀ PHÊ, TRÀ SỮA</div>
                  <div className="text-2xl font-bold mt-1">
                    CHỈ TỪ <span className="text-yellow-300">10,000</span>
                  </div>
                  <div className="text-xl font-bold">ĐỒNG</div>
                </div>
                <div className="w-20 h-20 bg-white rounded-lg flex items-center justify-center">
                  <span className="text-4xl">🍵</span>
                </div>
              </div>
            </div>

            {/* Bottom Small Banner */}
            <div className="bg-linear-to-r from-red-500 to-orange-500 rounded-lg p-4 shadow-lg flex-1 relative overflow-hidden">
              <div className="absolute top-2 left-2 bg-white px-2 py-1 rounded text-xs font-bold text-red-600">15.1</div>
              <div className="flex items-center justify-between h-full">
                <div className="text-white">
                  <div className="text-lg font-bold">SHOPEE</div>
                  <div className="text-2xl font-black text-yellow-300">REWARDS</div>
                  <div className="text-xs mt-2 bg-white text-red-600 px-2 py-1 rounded inline-block">
                    Giảm <span className="font-bold">12%</span>
                  </div>
                </div>
                <div className="text-6xl">🎁</div>
              </div>
              <div className="absolute -top-4 -right-4 bg-blue-600 text-white px-3 py-1 rounded-lg transform rotate-12 text-xs font-bold">
                FREESHIP 0Đ
              </div>
            </div>
          </div>
        </div>
        <div className="grid grid-cols-3 md:grid-cols-6 gap-4 p-6 bg-white border-t">
          {navigationItems.map((item, index) => (
            <button
              key={index}
              className="flex flex-col items-center space-y-2 p-3 rounded-lg hover:bg-gray-50 transition-colors cursor-pointer group"
            >
              <div className="w-12 h-12 rounded-full bg-orange-50 flex items-center justify-center group-hover:bg-orange-100 transition-colors">
                {item.icon}
              </div>
              <span className="text-xs text-center text-gray-700 font-medium">{item.label}</span>
            </button>
          ))}
        </div>
      </Container>
    </div>
  );
};
