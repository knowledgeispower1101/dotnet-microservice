import { Container } from '@/components';
import { Link, useParams } from 'react-router-dom';

const CategoryPage = () => {
  const { id } = useParams<{ id: string }>();
  return (
    <div>
      <Container>
        <div>BannerSimple</div>
        <div>Carousel</div>
        <div>curated-collection {id}</div>
      </Container>
      <Container className="flex">
        <div className="flex-none basis-47.5 min-w-0 mr-5 mb-5">
          <div>
            <Link
              to="/"
              className="flex items-center h-12.5 leading-12.5 mb-2.5 text-[1rem] font-bold capitalize text-[#000c] border-b border-[#0000000d] no-underline"
            >
              <svg viewBox="0 0 12 10" className="w-3 mr-2.5">
                <g fillRule="evenodd" stroke="none" strokeWidth="1">
                  <g transform="translate(-373 -208)">
                    <g transform="translate(155 191)">
                      <g transform="translate(218 17)">
                        <path d="m0 2h2v-2h-2zm4 0h7.1519633v-2h-7.1519633z"></path>
                        <path d="m0 6h2v-2h-2zm4 0h7.1519633v-2h-7.1519633z"></path>
                        <path d="m0 10h2v-2h-2zm4 0h7.1519633v-2h-7.1519633z"></path>
                      </g>
                    </g>
                  </g>
                </g>
              </svg>
              Tất cả Danh mục
            </Link>
          </div>
        </div>
        <div className="flex-1 min-w-0">
          <section>
            <fieldset className="flex items-center justify-between bg-black/5 rounded-sm px-5 py-3 font-normal">
              <div className="text-[#555] mr-1.25">Sắp xếp theo</div>
            </fieldset>
          </section>
        </div>
      </Container>
    </div>
  );
};

export default CategoryPage;
