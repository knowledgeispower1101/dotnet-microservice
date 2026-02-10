import { useCallback } from 'react';
import type { IconType } from 'react-icons/lib';
import { useSearchParams } from 'react-router-dom';
import qs from 'query-string';
import { router } from '@/app';

interface CategoryBoxProps {
  icon: IconType;
  label: string;
  selected?: boolean;
  description: string;
}

function CategoryBox({ icon: Icon, label, selected }: CategoryBoxProps) {
  const [searchParams] = useSearchParams();

  const handleOnClick = useCallback(() => {
    const currentQuery = qs.parse(searchParams.toString());

    const updatedQuery: Record<string, string | string[] | undefined> = {
      ...currentQuery,
      category: label,
    };

    if (searchParams.get('category') === label) {
      delete updatedQuery.category;
    }

    const url = qs.stringifyUrl(
      {
        url: '/',
        query: updatedQuery,
      },
      { skipNull: true },
    );

    router.navigate(url);
  }, [searchParams, label]);

  return (
    <div
      onClick={handleOnClick}
      className={`flex 
        flex-col 
        items-center 
        justify-center 
        gap-2 
        p-3 
        border-b-2 
        hover:text-neutral-800 
        transition 
        cursor-pointer
        ${selected ? 'border-b-neutral-800' : 'border-transparent'}
        ${selected ? 'text-neutral-800' : 'text-neutral-500'}`}
    >
      <Icon size={26} />
      <div className="font-medium text-sm">{label}</div>
    </div>
  );
}

export default CategoryBox;
