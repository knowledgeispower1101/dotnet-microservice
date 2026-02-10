import { api } from '@/lib';

export interface ImageItem {
  id: string;
  file: File;
  contentType: string;
  preview: string;
}

export interface ImageUploadProps {
  setImage: ImageItem[];
  onChange: (value: ImageItem[]) => void;
}

export const mediaApi = {
  upload: async (payload: File) => {
    const formData = new FormData();
    formData.append('file', payload);
    const res = await api.post('/media/image', formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return res.data;
  },
};
