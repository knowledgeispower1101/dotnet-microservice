import { Container, DividerLayout } from '@/components';
import { useParams } from 'react-router-dom';
import { LeftSideCategory, RightSideCategory } from './components';

const CategoryPage = () => {
  const { id } = useParams<{ id: string }>();
  if (!id) return null;
  return (
    <div>
      <Container>
        <div className="bg-[url('/src/assets/bg-sale-cate.png')] bg-contain h-90 bg-no-repeat mt-8"></div>
        <div>Carousel</div>
        <div>curated-collection {id}</div>
      </Container>
      <DividerLayout>
        <DividerLayout.LeftSide>
          <LeftSideCategory id={id} />
        </DividerLayout.LeftSide>
        <DividerLayout.RightSide>
          <RightSideCategory />
        </DividerLayout.RightSide>
      </DividerLayout>
    </div>
  );
};

export default CategoryPage;
