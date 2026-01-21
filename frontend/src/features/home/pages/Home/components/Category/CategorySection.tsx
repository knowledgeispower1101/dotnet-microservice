import { MyCarousel } from '@/components';
import { useNavigate } from 'react-router-dom';

export interface Category {
  id: number;
  name: string;
  icon: string;
}

const categories: Category[] = [
  { id: 1, name: 'Máy Ảnh & Máy Quay Phim', icon: '📷' },
  { id: 2, name: 'Đồng Hồ', icon: '⌚' },
  { id: 3, name: 'Giày Dép Nam', icon: '👟' },
  { id: 4, name: 'Thiết Bị Điện Gia Dụng', icon: '🔌' },
  { id: 5, name: 'Thể Thao & Du Lịch', icon: '⚽' },
  { id: 6, name: 'Ô Tô & Xe Máy & Xe Đạp', icon: '🛵' },
  { id: 7, name: 'Balo & Túi Ví Nam', icon: '🎒' },
  { id: 8, name: 'Đồ Chơi', icon: '🧸' },
  { id: 9, name: 'Chăm Sóc Thú Cưng', icon: '🐾' },
  { id: 10, name: 'Dụng cụ và thiết bị tiện ích', icon: '🔧' },
  { id: 11, name: 'Sức Khỏe', icon: '💊' },
  { id: 12, name: 'Giày Dép Nữ', icon: '👠' },
  { id: 13, name: 'Túi Ví Nữ', icon: '👜' },
  { id: 14, name: 'Phụ Kiện & Trang Sức Nữ', icon: '💍' },
  { id: 15, name: 'Bách Hóa Online', icon: '🍫' },
  { id: 16, name: 'Nhà Sách Online', icon: '📚' },
  { id: 17, name: 'Thời Trang Trẻ Em', icon: '👶' },
  { id: 18, name: 'Giặt Giũ & Chăm Sóc Nhà Cửa', icon: '🧴' },
  { id: 19, name: 'Voucher & Dịch Vụ', icon: '🎫' },
  { id: 20, name: 'Thời Trang Nam', icon: '👕' },
  { id: 21, name: 'Điện Thoại & Phụ Kiện', icon: '📱' },
  { id: 22, name: 'Thiết Bị Điện Tử', icon: '🖥️' },
  { id: 23, name: 'Máy Tính & Laptop', icon: '💻' },
  { id: 24, name: 'Thời Trang Nữ', icon: '👗' },
  { id: 25, name: 'Mẹ & Bé', icon: '👪' },
  { id: 26, name: 'Nhà Cửa & Đời Sống', icon: '🍳' },
  { id: 27, name: 'Sắc Đẹp', icon: '💄' },
  { id: 28, name: 'Thực Phẩm & Đồ Uống', icon: '🍔' },
  { id: 29, name: 'Văn Phòng Phẩm', icon: '✏️' },
  { id: 30, name: 'Thiết Bị Âm Thanh', icon: '🎧' },
];
const CategoryItem = ({ category }: { category: Category }) => {
  const navigate = useNavigate();

  return (
    <div
      onClick={() => navigate(`/category/${category.id}`)}
      className="flex flex-col items-center justify-center cursor-pointer
                 hover:shadow-md transition-shadow group"
    >
      <div className="w-30 h-30 flex items-center justify-center bg-gray-50 rounded-lg mb-2">
        <span className="text-5xl">{category.icon}</span>
      </div>

      <span className="text-sm text-center line-clamp-2">{category.name}</span>
    </div>
  );
};
const CategorySection = () => {
  return (
    <div className="bg-white">
      <div className="max-w-300 mx-auto px-5 py-4">
        <div className="text-base font-medium text-[#0000008a] uppercase h-15 flex items-center">Danh Mục</div>

        <div className="relative overflow-hidden">
          <MyCarousel items={categories} itemsPerRow={8} rows={2} renderItem={(item) => <CategoryItem category={item} />} />
        </div>
      </div>
    </div>
  );
};

export default CategorySection;
// items={(category: Category) => }
