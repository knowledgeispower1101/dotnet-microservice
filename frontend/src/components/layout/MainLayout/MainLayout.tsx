import { Outlet } from 'react-router-dom';
import { Footer, HeaderLayout } from '@/components';

const MainLayout = () => {
  return (
    <>
      <HeaderLayout />
      <main>
        <Outlet />
      </main>
      <Footer />
    </>
  );
};
export default MainLayout;
