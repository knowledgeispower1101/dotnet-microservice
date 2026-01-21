import { type RouteObject } from 'react-router-dom';
import { MainLayout } from '@/components';
import { lazy } from 'react';
const HomePage = lazy(() => import('./pages/Home/HomePage'));
export const homeRoutes: RouteObject[] = [
  {
    path: '/',
    element: <MainLayout />,
    children: [
      {
        index: true,
        element: <HomePage />,
      },
    ],
  },
];
