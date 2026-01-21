import { useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import { Header, Navbar } from './components';

const HeaderLayout = () => {
  const location = useLocation();
  useEffect(() => {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }, [location.pathname]);

  return (
    <>
      <Navbar />
      <Header />
    </>
  );
};

export default HeaderLayout;
