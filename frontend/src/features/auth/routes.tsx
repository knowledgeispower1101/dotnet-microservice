import { type RouteObject } from 'react-router-dom';
import { AuthenticationLayout } from '@/components';
import { lazy } from 'react';
const LoginElement = lazy(() => import('./components/Login'));
const RegisterElement = lazy(() => import('./components/Register'));

export const authRoutes: RouteObject[] = [
  {
    path: 'auth',
    element: <AuthenticationLayout />,
    children: [
      {
        path: 'login',
        element: <LoginElement />,
      },
      {
        path: 'register',
        element: <RegisterElement />,
      },
    ],
  },
];
