import { MyCarousel } from '@/components';
import { useFetch } from '@/hooks';
import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

export interface Category {
  id: number;
  name: string;
  iconUrl: string;
}

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
  const { data, execute } = useFetch<Category[]>();

  useEffect(() => {
    execute({
      url: '/ecommerce/category',
      method: 'GET',
    });
  }, [execute]);
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
