import { Navbar, RegisterModal } from '@/components';
import { ToasterProvider } from '@/providers';

const MainLayout = () => {
  return (
    <>
      <ToasterProvider />
      <Navbar />
      <RegisterModal />
      {/* 
      <main>
        <Outlet />
      </main>
      <Footer /> */}
    </>
  );
};
export default MainLayout;
