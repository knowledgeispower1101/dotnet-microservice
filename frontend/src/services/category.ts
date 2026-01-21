import { api } from '@/lib';

export interface Category {
  id: number;
  name: string;
  level: number;
  is_leaf: boolean;
  image_key: string;
}

export const categoryApi = {
  getMenu: async (): Promise<Category[]> => {
    const res = await api.get('/categories/menu');
    return res.data.data;
  },
  getCategoryTree: async ({ id }: { id: number }): Promise<Category[]> => {
    const res = await api.get(`/categories/tree/by-product/${id}`);
    return res.data.data;
  },
};
