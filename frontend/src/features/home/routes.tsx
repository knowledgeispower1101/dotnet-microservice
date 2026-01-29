import { type RouteObject } from 'react-router-dom';
import { MainLayout } from '@/components';
export const homeRoutes: RouteObject[] = [
  {
    path: '/',
    element: <MainLayout />,
    children: [],
  },
];
