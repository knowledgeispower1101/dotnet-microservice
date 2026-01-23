import { CategoryIcon } from '@/assets';
import { useGetCategoriesChildren } from '@/hooks';
import { Link } from 'react-router-dom';

const LeftSideCategory = ({ id }: { id: string }) => {
  const { data, isLoading, error } = useGetCategoriesChildren({ id });

  if (isLoading) return <div>Loading...</div>;
  if (error) return <div>Error</div>;
  if (!data || data.length === 0) return null;
  const [firstItem, ...restItems] = data;

  return (
    <>
      <div>
        <Link
          to="/"
          className="flex items-center h-12.5 leading-12.5 mb-2.5 text-[1rem] font-bold capitalize text-[#000c] border-b border-[#0000000d] no-underline"
        >
          <CategoryIcon />
          Tất cả Danh mục
        </Link>
      </div>

      <div className="flex flex-col font-bold text-sm">
        <div className="text-[#EE4D2D] pt-2 pb-2 pr-2.5 pl-3">{firstItem.name}</div>

        {/* 👉 Các item còn lại */}
        <div className="flex flex-col">
          {restItems.map((item) => (
            <Link key={item.id} to={`/category/${item.id}`} className="pt-2 pb-2 pr-2.5 pl-3">
              {item.name}
            </Link>
          ))}
        </div>
      </div>
    </>
  );
};

export default LeftSideCategory;
