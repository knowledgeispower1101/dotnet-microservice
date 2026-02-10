import { Counter, Heading } from '@/components';

interface InfoProps {
  guestCount: number;
  roomCount: number;
  bathroomCount: number;
  onGuestChange: (value: number) => void;
  onRoomChange: (value: number) => void;
  onBathroomChange: (value: number) => void;
}
function InforStep({
  guestCount,
  roomCount,
  bathroomCount,
  onGuestChange,
  onRoomChange,
  onBathroomChange,
}: InfoProps) {
  return (
    <div className="flex flex-col gap-8">
      <Heading title="Share some basics about your place" subtitle="What amenities do you have?" />

      <Counter
        title="Guests"
        subtitle="How many guests do you allow?"
        value={guestCount}
        onChange={onGuestChange}
      />

      <Counter
        title="Rooms"
        subtitle="How many rooms do you have?"
        value={roomCount}
        onChange={onRoomChange}
      />

      <Counter
        title="Bathrooms"
        subtitle="How many bathrooms do you have?"
        value={bathroomCount}
        onChange={onBathroomChange}
      />
    </div>
  );
}

export default InforStep;
