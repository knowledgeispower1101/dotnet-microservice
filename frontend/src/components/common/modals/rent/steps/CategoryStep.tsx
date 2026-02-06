import { CategoryInput, Heading } from '@/components';
import { categories } from '@/constants';

interface CategoryProps {
  category: string;
  onSelect: (value: string) => void;
}

function CategoryStep({ category, onSelect }: CategoryProps) {
  return (
    <div className="flex flex-col gap-8">
      <Heading title="Which of these best describes your place?" subtitle="Pick a category" />

      <div className="grid grid-cols-1 md:grid-cols-2 gap-3 max-h-[50vh] overflow-y-auto">
        {categories.map(({ icon, label }) => (
          <div key={label} className="col-span-1">
            <CategoryInput
              selected={category === label}
              onClick={onSelect}
              icon={icon}
              label={label}
            />
          </div>
        ))}
      </div>
    </div>
  );
}

export default CategoryStep;
