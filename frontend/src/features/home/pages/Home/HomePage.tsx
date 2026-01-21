import { Container } from '@/components';
import { BannerSection, CategorySection, FlashSaleCarousel } from './components';

const HomePage = () => {
  return (
    <div>
      <BannerSection />
      <div className="bg-[#F5F5F5] pt-2.5">
        <Container>
          <CategorySection />
          <FlashSaleCarousel />
        </Container>
      </div>
    </div>
  );
};

export default HomePage;
