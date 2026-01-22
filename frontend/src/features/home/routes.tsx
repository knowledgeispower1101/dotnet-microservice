import { type RouteObject } from 'react-router-dom';
import { MainLayout } from '@/components';
import { lazy } from 'react';
const HomePage = lazy(() => import('./pages/Home/HomePage'));
const CategoryPage = lazy(() => import('./pages/Category/CategoryPage'));
export const homeRoutes: RouteObject[] = [
  {
    path: '/',
    element: <MainLayout />,
    children: [
      {
        path: 'category/:id',
        element: <CategoryPage />,
      },
      {
        index: true,
        element: <HomePage />,
      },
    ],
  },
];
