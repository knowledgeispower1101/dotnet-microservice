import { createBrowserRouter } from 'react-router-dom';
import { authRoutes, homeRoutes } from '@/features';

export const router = createBrowserRouter([...authRoutes, ...homeRoutes]);
