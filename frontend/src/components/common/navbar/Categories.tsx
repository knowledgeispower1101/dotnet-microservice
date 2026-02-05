import { useLocation, useSearchParams } from 'react-router-dom';
import { categories } from '@/constants';
import CategoryBox from './CategoryBox';
import Container from '../Container';

function Categories() {
  const [searchParams] = useSearchParams();
  const category = searchParams.get('category');
  const location = useLocation();
  const pathname = location.pathname;
  const isMainPage = pathname === '/';

  if (!isMainPage) return null;
  return (
    <Container>
      <div className="pt-4 flex flex-row items-center justify-between overflow-x-auto">
        {categories.map(({ label, description, icon }) => (
          <CategoryBox key={label} label={label} description={description} icon={icon} selected={category === label} />
        ))}
      </div>
    </Container>
  );
}

export default Categories;
