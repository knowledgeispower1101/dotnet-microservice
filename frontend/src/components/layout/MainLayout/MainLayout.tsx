import { LoginModal, Navbar, RegisterModal, RentModal } from '@/components';
import { useAuthInit } from '@/hooks';
import { ToasterProvider } from '@/providers';

const MainLayout = () => {
  useAuthInit();
  return (
    <>
      <ToasterProvider />
      <Navbar />
      <RentModal />
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
