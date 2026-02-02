import { LoginModal, Navbar, RegisterModal } from '@/components';
import { useAuthInit } from '@/hooks';
import { ToasterProvider } from '@/providers';

const MainLayout = () => {
  useAuthInit();
  return (
    <>
      <ToasterProvider />
      <Navbar />
      <LoginModal />
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
