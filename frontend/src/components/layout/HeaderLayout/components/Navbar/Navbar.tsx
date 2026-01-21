import { Link } from 'react-router-dom';

const Navbar = () => {
  return (
    <nav className="bg-[#F6412D] text-white text-xs sticky top-0 z-50 w-full h-8">
      <div className="max-w-300 mx-auto h-8.5 px-4 flex items-center justify-between">
        {/* LEFT */}
        <div className="flex items-center gap-2">
          <Link to="/" className="hover:underline">
            Kênh Người Bán
          </Link>
          <span className="opacity-70">|</span>

          <Link to="/" className="hover:underline">
            Trở thành Người bán Shopee
          </Link>
          <span className="opacity-70">|</span>

          <Link to="/" className="hover:underline">
            Tải ứng dụng
          </Link>
          <span className="opacity-70">|</span>

          <div className="flex items-center gap-1">
            <span>Kết nối</span>
            <span className="w-4 h-4 bg-white/90 rounded-sm" />
            <span className="w-4 h-4 bg-white/90 rounded-sm" />
          </div>
        </div>

        {/* RIGHT */}
        <div className="flex items-center gap-2">
          <Link to="/" className="hover:underline">
            Thông Báo
          </Link>
          <span className="opacity-70">|</span>

          <Link to="/" className="hover:underline">
            Hỗ Trợ
          </Link>
          <span className="opacity-70">|</span>

          <Link to="/" className="hover:underline">
            Tiếng Việt
          </Link>
          <span className="opacity-70">|</span>

          <Link to="auth/register" className="hover:underline font-semibold">
            Đăng Ký
          </Link>
          <span className="opacity-70">|</span>

          <Link to="auth/login" className="hover:underline font-semibold">
            Đăng Nhập
          </Link>
        </div>
      </div>
    </nav>
  );
};
export default Navbar;
