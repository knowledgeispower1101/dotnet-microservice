import React, { useState, useMemo } from 'react';
// import { ChevronLeft, ChevronRight } from 'lucide-react';

interface CarouselProps<T> {
  items: T[];
  renderItem: (item: T, index: number) => React.ReactNode;
  itemsPerRow?: number;
  rows?: number;
  gap?: number;
  showDots?: boolean;
  showArrows?: boolean;
}

export const MyCarousel = <T,>({ items, renderItem, itemsPerRow = 3, rows = 1, gap = 16, showArrows = true }: CarouselProps<T>) => {
  const [currentIndex, setCurrentIndex] = useState<number>(0);

  const itemsPerPage: number = itemsPerRow * rows;
  const totalPages: number = Math.ceil(items.length / itemsPerPage);

  const nextSlide = (): void => {
    setCurrentIndex((prev) => (prev + 1 >= totalPages ? 0 : prev + 1));
  };

  const prevSlide = (): void => {
    setCurrentIndex((prev) => (prev - 1 < 0 ? totalPages - 1 : prev - 1));
  };

  const visibleItems: T[] = useMemo(
    () => items.slice(currentIndex * itemsPerPage, (currentIndex + 1) * itemsPerPage),
    [items, currentIndex, itemsPerPage],
  );

  return (
    <div className="w-full">
      <div className="relative px-12">
        {showArrows && totalPages > 1 && currentIndex + 1 > 1 && (
          <button
            onClick={prevSlide}
            className="absolute left-0 top-1/2 -translate-y-1/2 z-10 bg-white hover:bg-gray-100 rounded-full p-2 shadow-lg transition-all"
            aria-label="Previous"
          >
            {/* <ChevronLeft className="w-5 h-5 text-gray-700" /> */}
          </button>
        )}

        <div
          className="grid transition-all duration-300"
          style={{
            gridTemplateColumns: `repeat(${itemsPerRow}, 1fr)`,
            gap: `${gap}px`,
          }}
        >
          {visibleItems.map((item, index) => (
            <div key={currentIndex * itemsPerPage + index}>{renderItem(item, index)}</div>
          ))}
        </div>

        {showArrows && totalPages > 1 && currentIndex + 1 < totalPages && (
          <button
            onClick={nextSlide}
            className="absolute right-0 top-1/2 -translate-y-1/2 z-10 bg-white hover:bg-gray-100 rounded-full p-2 shadow-lg transition-all"
            aria-label="Next"
          >
            {/* <ChevronRight className="w-5 h-5 text-gray-700" /> */}
          </button>
        )}
      </div>

      {/* {showDots && totalPages > 1 && (
        <div className="flex justify-center gap-2 mt-6">
          {Array.from({ length: totalPages }).map((_, idx) => (
            <button
              key={idx}
              onClick={() => setCurrentIndex(idx)}
              className={`w-2 h-2 rounded-full transition-all ${idx === currentIndex ? 'bg-blue-600 w-8' : 'bg-gray-300 hover:bg-gray-400'}`}
              aria-label={`Go to slide ${idx + 1}`}
            />
          ))}
        </div>
      )} */}
    </div>
  );
};
