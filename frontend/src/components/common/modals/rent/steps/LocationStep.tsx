import { CountrySelect, Heading, type CountrySelectValue } from '@/components';
import { lazy, Suspense } from 'react';
interface LocationProps {
  location: CountrySelectValue;
  onLocationChange: (value: CountrySelectValue | null) => void;
}
const Map = lazy(() => import('../../../Map'));
function LocationStep({ location, onLocationChange }: LocationProps) {
  return (
    <div className="flex flex-col gap-8">
      <Heading title="Where is your place located?" subtitle="Help guests find you!" />
      <CountrySelect value={location} onChange={onLocationChange} />
      <Suspense fallback={<div className="rounded-lg bg-neutral-200" />}>
        <Map key={location?.value || 'default'} center={location?.latlng} />
      </Suspense>
    </div>
  );
}

export default LocationStep;
