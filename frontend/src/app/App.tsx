import { Loading } from '@/components';
import { Suspense } from 'react';
import { RouterProvider } from 'react-router-dom';
import { router } from './router';

export default function App() {
  return (
    <Suspense fallback={<Loading />}>
      <RouterProvider router={router} />
    </Suspense>
  );
}
