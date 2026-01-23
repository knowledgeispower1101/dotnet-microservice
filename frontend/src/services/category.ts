import { api } from '@/lib';

export interface Category {
  id: number;
  name: string;
  image_key: string;
}

export const categoryApi = {
  getMenu: async (): Promise<Category[]> => {
    const res = await api.get('/ecommerce/category');
    return res.data.data;
  },
  getCategoryChildren: async ({ id }: { id: string }): Promise<Category[]> => {
    const res = await api.get(`/ecommerce/category/${id}/children`);
    return res.data;
  },
};
