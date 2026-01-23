import { categoryApi } from '@/services';
import { useQuery } from '@tanstack/react-query';

export const useGetCategoriesChildren = ({ id }: { id: string }) => {
  return useQuery({
    queryKey: ['categories', 'children', id],
    queryFn: () => categoryApi.getCategoryChildren({ id }),
    enabled: !!id,
  });
};
