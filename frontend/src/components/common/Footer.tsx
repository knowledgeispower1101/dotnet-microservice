import React from 'react';
import { Facebook, Instagram, Linkedin } from 'lucide-react';
const customerService = [
  'Trung Tâm Trợ Giúp Shopee',
  'Shopee Blog',
  'Shopee Mall',
  'Hướng Dẫn Mua Hàng/Đặt Hàng',
  'Hướng Dẫn Bán Hàng',
  'Ví ShopeePay',
  'Shopee Xu',
  'Đơn Hàng',
  'Trả Hàng/Hoàn Tiền',
  'Liên Hệ Shopee',
  'Chính Sách Bảo Hành',
];

const aboutShopee = [
  'Về Shopee',
  'Tuyển Dụng',
  'Điều Khoản Shopee',
  'Chính Sách Bảo Mật',
  'Shopee Mall',
  'Kênh Người Bán',
  'Flash Sale',
  'Tiếp Thị Liên Kết',
  'Liên Hệ Truyền Thông',
];

export const Footer = () => {
  const countries = ['Argentina', 'Singapore', 'Indonesia', 'Thái Lan', 'Malaysia', 'Việt Nam', 'Philippines', 'Brazil', 'México', 'Đài Loan'];
  console.log('render Footer layout');

  return (
    <footer className="bg-gray-100 border-t border-orange-500">
      <div className="max-w-7xl mx-auto px-4 py-8">
        {/* Main Footer Content */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-5 gap-8 mb-8">
          {/* Customer Service */}
          <div>
            <h3 className="font-bold text-sm mb-4 text-gray-800">DỊCH VỤ KHÁCH HÀNG</h3>
            <ul className="space-y-2">
              {customerService.map((item, index) => (
                <li key={index}>
                  <a href="#" className="text-gray-600 text-xs hover:text-orange-500 transition-colors">
                    {item}
                  </a>
                </li>
              ))}
            </ul>
          </div>

          {/* About Shopee */}
          <div>
            <h3 className="font-bold text-sm mb-4 text-gray-800">SHOPEE VIỆT NAM</h3>
            <ul className="space-y-2">
              {aboutShopee.map((item, index) => (
                <li key={index}>
                  <a
                    href="#"
                    className={`text-xs hover:text-orange-500 transition-colors ${item === 'Kênh Người Bán' ? 'text-orange-500' : 'text-gray-600'}`}
                  >
                    {item}
                  </a>
                </li>
              ))}
            </ul>
          </div>

          {/* Payment Methods */}
          <div>
            <h3 className="font-bold text-sm mb-4 text-gray-800">THANH TOÁN</h3>
            <div className="grid grid-cols-3 gap-2">
              <div className="bg-white p-2 rounded border border-gray-200 flex items-center justify-center h-10">
                <span className="text-blue-800 font-bold text-xs">VISA</span>
              </div>
              <div className="bg-white p-2 rounded border border-gray-200 flex items-center justify-center h-10">
                <span className="text-red-600 font-bold text-xs">MC</span>
              </div>
              <div className="bg-white p-2 rounded border border-gray-200 flex items-center justify-center h-10">
                <span className="text-blue-600 font-bold text-xs">JCB</span>
              </div>
              <div className="bg-white p-2 rounded border border-gray-200 flex items-center justify-center h-10">
                <span className="text-blue-700 text-xs">AMEX</span>
              </div>
              <div className="bg-white p-2 rounded border border-gray-200 flex items-center justify-center h-10">
                <span className="text-orange-500 text-xs">COD</span>
              </div>
              <div className="bg-white p-2 rounded border border-gray-200 flex items-center justify-center h-10">
                <span className="text-orange-500 font-bold text-xs">₫</span>
              </div>
              <div className="bg-white p-2 rounded border border-gray-200 flex items-center justify-center h-10">
                <span className="text-orange-600 text-xs">SPay</span>
              </div>
              <div className="bg-white p-2 rounded border border-gray-200 flex items-center justify-center h-10">
                <span className="text-orange-500 text-xs">SPayL</span>
              </div>
            </div>
          </div>

          {/* Follow Us */}
          <div>
            <h3 className="font-bold text-sm mb-4 text-gray-800">THEO DÕI SHOPEE</h3>
            <ul className="space-y-3">
              <li>
                <a href="#" className="flex items-center text-gray-600 text-xs hover:text-orange-500 transition-colors">
                  <Facebook className="w-4 h-4 mr-2" />
                  Facebook
                </a>
              </li>
              <li>
                <a href="#" className="flex items-center text-gray-600 text-xs hover:text-orange-500 transition-colors">
                  <Instagram className="w-4 h-4 mr-2" />
                  Instagram
                </a>
              </li>
              <li>
                <a href="#" className="flex items-center text-gray-600 text-xs hover:text-orange-500 transition-colors">
                  <Linkedin className="w-4 h-4 mr-2" />
                  LinkedIn
                </a>
              </li>
            </ul>
          </div>

          {/* App Download */}
          <div>
            <h3 className="font-bold text-sm mb-4 text-gray-800">TẢI ỨNG DỤNG SHOPEE</h3>
            <div className="flex gap-2 mb-4">
              <div className="bg-white p-2 rounded border border-gray-200">
                <div className="w-16 h-16 bg-gray-200 flex items-center justify-center text-xs text-gray-500">QR Code</div>
              </div>
              <div className="flex flex-col gap-2">
                <div className="bg-black text-white px-3 py-1 rounded text-xs flex items-center gap-1">
                  <span>🍎</span> App Store
                </div>
                <div className="bg-black text-white px-3 py-1 rounded text-xs flex items-center gap-1">
                  <span>▶</span> Google Play
                </div>
                <div className="bg-black text-white px-3 py-1 rounded text-xs flex items-center gap-1">
                  <span>📱</span> AppGallery
                </div>
              </div>
            </div>
          </div>
        </div>

        <div className="mb-8">
          <h3 className="font-bold text-sm mb-4 text-gray-800">ĐƠN VỊ VẬN CHUYỂN</h3>
          <div className="grid grid-cols-4 md:grid-cols-9 gap-3">
            {['SPX', 'Express', 'Viettel', 'VN Post', 'J&T', 'GrabExpress', 'Ninjavan', 'Be', 'Ahamove'].map((item, index) => (
              <div key={index} className="bg-white p-2 rounded border border-gray-200 flex items-center justify-center h-12">
                <span className="text-xs text-gray-600">{item}</span>
              </div>
            ))}
          </div>
        </div>

        <div className="border-t border-gray-300 pt-6">
          <div className="flex flex-wrap items-center justify-between mb-4">
            <p className="text-xs text-gray-600">© 2026 Shopee. Tất cả các quyền được bảo lưu.</p>
            <div className="flex flex-wrap gap-2 text-xs text-gray-600">
              <span className="font-semibold">Quốc gia & Khu vực:</span>
              {countries.map((country, index) => (
                <React.Fragment key={country}>
                  <a href="#" className="hover:text-orange-500 transition-colors">
                    {country}
                  </a>
                  {index < countries.length - 1 && <span>|</span>}
                </React.Fragment>
              ))}
            </div>
          </div>

          <div className="flex flex-wrap justify-center gap-6 mb-6 text-xs text-gray-600">
            <a href="#" className="hover:text-orange-500 transition-colors">
              CHÍNH SÁCH BẢO MẬT
            </a>
            <a href="#" className="hover:text-orange-500 transition-colors">
              QUY CHẾ HOẠT ĐỘNG
            </a>
            <a href="#" className="hover:text-orange-500 transition-colors">
              CHÍNH SÁCH VẬN CHUYỂN
            </a>
            <a href="#" className="hover:text-orange-500 transition-colors">
              CHÍNH SÁCH TRẢ HÀNG VÀ HOÀN TIỀN
            </a>
          </div>

          <div className="flex justify-center gap-4 mb-4">
            <div className="bg-white p-2 rounded border border-gray-200">
              <div className="w-12 h-12 bg-red-600 rounded-full flex items-center justify-center text-white text-xs font-bold">ĐK</div>
            </div>
            <div className="bg-white p-2 rounded border border-gray-200">
              <div className="w-12 h-12 bg-red-600 rounded-full flex items-center justify-center text-white text-xs font-bold">ĐK</div>
            </div>
            <div className="bg-white p-2 rounded border border-gray-200">
              <div className="w-12 h-12 bg-orange-600 rounded-full flex items-center justify-center text-white text-xs">🏛</div>
            </div>
          </div>

          <div className="text-center text-xs text-gray-600 space-y-1">
            <p className="font-semibold">Công ty TNHH Shopee</p>
            <p>Địa chỉ: Tầng 4-5-6, Tòa nhà Capital Place, số 29 đường Liễu Giai, Phường Ngọc Hà, Thành phố Hà Nội, Việt Nam</p>
            <p>Chăm sóc khách hàng: Gọi tổng đài Shopee (miễn phí) hoặc Trò chuyện với Shopee ngay trên Trung tâm trợ giúp</p>
            <p>Chịu Trách Nhiệm Quản Lý Nội Dung: Nguyễn Bùi Anh Tuấn</p>
            <p>Mã số doanh nghiệp: 0106773786 do Sở Kế hoạch và Đầu tư TP Hà Nội cấp lần đầu ngày 10/02/2015</p>
            <p>© 2015 - Bản quyền thuộc về Công ty TNHH Shopee</p>
          </div>
        </div>
      </div>

      <div className="fixed bottom-6 right-6">
        <button className="bg-orange-500 text-white px-4 py-3 rounded-full shadow-lg hover:bg-orange-600 transition-colors flex items-center gap-2">
          <span className="text-2xl">💬</span>
          <span className="font-semibold">Chat</span>
        </button>
      </div>
    </footer>
  );
};
