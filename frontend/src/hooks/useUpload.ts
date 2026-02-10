import { mediaApi } from '@/services/media';
import { useMutation } from '@tanstack/react-query';

const uploadHooks = {
  useUploadImage: () =>
    useMutation<string, Error, File>({
      mutationFn: (file) => mediaApi.upload(file),
    }),

  useUploadImages: () =>
    useMutation<string[], Error, File[]>({
      mutationFn: async (files) => {
        const requests = files.map((file) => mediaApi.upload(file));
        return Promise.all(requests);
      },
    }),
};

export default uploadHooks;
