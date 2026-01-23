import { Container, DividerLayout, Loading } from '@/components';
import { LeftSideCategory, RightSideCategory } from './components';
import { useParams } from 'react-router-dom';
import { useGetCategoriesChildren } from '@/hooks';

const CategoryPage = () => {
  const handleChangeCategory = (id: string) => {
    //get products base on category id and pass to right side category
    console.log(id);
  };
  const params = useParams<{ id?: string }>();
  const id = params.id ?? '';
  const { data: categoryChildren, isLoading, error } = useGetCategoriesChildren({
    id,
  });
  
  if (isLoading) return <Loading />;
  if (error) {
    return (
      <Container>
        <div className="flex flex-col items-center justify-center min-h-96 gap-4">
          <p className="text-red-500 text-lg">Failed to load categories</p>
          <p className="text-gray-600 text-sm">Please try again later</p>
        </div>
      </Container>
    );
  }
  if (!categoryChildren || categoryChildren.length === 0) {
    return (
      <Container>
        <div className="flex flex-col items-center justify-center min-h-96">
          <p className="text-gray-600">No categories found</p>
        </div>
      </Container>
    );
  }
  return (
    <div>
      <Container>
        <div className="bg-[url('/src/assets/bg-sale-cate.png')] bg-contain h-90 bg-no-repeat mt-8"></div>
        <div>Carousel</div>
        <div>curated-collection</div>
      </Container>
      <DividerLayout>
        <DividerLayout.LeftSide>
          <LeftSideCategory data={categoryChildren ?? []} handleChangeCategory={handleChangeCategory} />
        </DividerLayout.LeftSide>
        <DividerLayout.RightSide>
          <RightSideCategory />
        </DividerLayout.RightSide>
      </DividerLayout>
    </div>
  );
};

export default CategoryPage;
