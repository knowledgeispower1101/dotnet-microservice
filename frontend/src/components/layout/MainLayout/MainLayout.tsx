import { LoginModal, Navbar, RegisterModal } from '@/components';
import RentModal from '@/components/common/modals/RentModal';
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
