import { Loading, MyCarousel } from '@/components';
import { useGetCategoriesMenu } from '@/hooks';
import type { Category } from '@/services';
import { useNavigate } from 'react-router-dom';

const CategoryItem = ({ category }: { category: Category }) => {
  const navigate = useNavigate();

  return (
    <div
      onClick={() => navigate(`/category/${category.id}`)}
      className="flex flex-col items-center justify-center cursor-pointer
                 hover:shadow-md transition-shadow group"
    >
      <div className="w-30 h-30 flex items-center justify-center bg-gray-50 rounded-lg mb-2">
        <span className="text-5xl">{category.iconUrl}</span>
      </div>

      <span className="text-sm text-center line-clamp-2">{category.name}</span>
    </div>
  );
};
const CategorySection = () => {
  const { data, isLoading } = useGetCategoriesMenu();
  if (isLoading) return <Loading />;
  return (
    <div className="bg-white">
      <div className="max-w-300 mx-auto px-5 py-4">
        <div className="text-base font-medium text-[#0000008a] uppercase h-15 flex items-center">Danh Mục</div>
        <div className="relative overflow-hidden">
          <MyCarousel items={data ?? []} itemsPerRow={8} rows={2} renderItem={(item) => <CategoryItem category={item} />} />
        </div>
      </div>
    </div>
  );
};

export default CategorySection;
