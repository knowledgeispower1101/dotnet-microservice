import { Search } from 'lucide-react';
import { useState, useMemo } from 'react';
import { useDebounce } from '@/hooks';
import { SEARCH_SUGGESTIONS } from '@/constants';

const SearchSection = () => {
  const [value, setValue] = useState('');
  const debouncedValue = useDebounce(value, 300);
  
  const filteredOptions = useMemo(
    () => SEARCH_SUGGESTIONS.filter((item) => item.toLowerCase().includes(debouncedValue.toLowerCase())),
    [debouncedValue]
  );

  return (
    <div className="relative w-210">
      <form className="flex w-full h-10 bg-white rounded-sm overflow-hidden">
        <input
          value={value}
          onChange={(e) => setValue(e.target.value)}
          placeholder="Tìm trong Điện Thoại & Phụ Kiện"
          className="flex-1 px-3 text-sm outline-none"
        />
        <button type="submit" className="w-15 bg-[#fb5533] flex items-center justify-center text-white">
          <Search />
        </button>
      </form>

      {debouncedValue && filteredOptions.length > 0 && (
        <ul className="absolute top-11 left-0 w-full bg-white shadow-lg border border-gray-200 z-50">
          {filteredOptions.map((item) => (
            <li key={item} onClick={() => setValue(item)} className="px-3 py-2 text-sm cursor-pointer hover:bg-gray-100">
              {item}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default SearchSection;
