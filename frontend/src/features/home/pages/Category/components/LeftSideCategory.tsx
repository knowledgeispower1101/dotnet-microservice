import { CategoryIcon } from '@/assets';
import type { Category } from '@/services';
import { Link } from 'react-router-dom';
interface LeftSideCategoryInterface {
  handleChangeCategory: (id: string) => void;
  data: Category[];
}
const LeftSideCategory = ({ handleChangeCategory, data }: LeftSideCategoryInterface) => {
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
            <div key={item.id} onClick={() => handleChangeCategory(item.id)} className="pt-2 pb-2 pr-2.5 pl-3">
              {item.name}
            </div>
          ))}
        </div>
      </div>
    </>
  );
};

export default LeftSideCategory;
