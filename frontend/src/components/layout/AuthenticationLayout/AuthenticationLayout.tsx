import { Container, Footer, Header } from '@/components';
import { Outlet } from 'react-router-dom';

const AuthenticationLayout = () => {
  return (
    <>
      <Header />
      <main className="bg-[#EE4D2D]">
        <Container className="bg-[url('/src/assets/bg-sale.png')] bg-contain h-150 bg-no-repeat bg-center bg-[#ee4d2d] flex items-center justify-end">
          <Outlet />
        </Container>
      </main>
      <Footer />
    </>
  );
};
export default AuthenticationLayout;
